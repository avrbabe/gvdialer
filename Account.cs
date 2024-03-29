﻿using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace GoogleVoice
{
    class Account
    {
        string _user;
        string _password;

        bool _loggedin;
        CookieContainer _cookies;
        string _rnr;

        static string LogonLandingUrl = "https://www.google.com/accounts/ServiceLoginAuth?service=grandcentral";
        static string LogonPostUrl = "https://accounts.google.com/ServiceLoginAuth";
        static string LogoutUrl = "https://www.google.com/voice/account/signout";
        static string MainUrl = "https://www.google.com/voice";
        static string CallUrl = "https://www.google.com/voice/call/connect";
        static string SMSUrl = "https://www.google.com/voice/sms/send";
        static string InboxUrl = "https://www.google.com/voice/inbox/recent/inbox/";
        static string AllCallsUrl = "https://www.google.com/voice/inbox/recent/all/";
        static string StarredCallsUrl = "https://www.google.com/voice/inbox/recent/starred/";
        static string SpamUrl = "https://www.google.com/voice/inbox/recent/spam/";
        static string TrashUrl = "https://www.google.com/voice/inbox/recent/trash/";
        static string VoicemailUrl = "https://www.google.com/voice/inbox/recent/voicemail/";
        static string SMSMessagesUrl = "https://www.google.com/voice/inbox/recent/sms/";
        static string RecordedCallsUrl = "https://www.google.com/voice/inbox/recent/recorded/";
        static string PlacedCallsUrl = "https://www.google.com/voice/inbox/recent/placed/";
        static string ReceivedCallsUrl = "https://www.google.com/voice/inbox/recent/received/";
        static string MissedCallsUrl = "https://www.google.com/voice/inbox/recent/missed/";
        static string ContactsUrl = "https://www.google.com/voice/inbox/recent/contacts/";
        static Uri CookieUri = new Uri("https://www.google.com");

        static Regex RNRExpression = new Regex(
            "name=\"_rnr_se\"\\s+(?:type=\"hidden\"\\s+)?value=\"([^\"]*)\"", RegexOptions.IgnoreCase);

        static Regex GALXExpression = new Regex(
            "name=\"GALX\"\\s+(?:type=\"hidden\"\\s+)?value=\"([^\"]*)\"", RegexOptions.IgnoreCase);

        static int SMSLength = 159;

        public Account(string user, string password)
        {
            _user = user;
            _password = password;

            _loggedin = false;
            _cookies = null;
            _rnr = null;
        }

        public bool Login()
        {
            if (_loggedin)
            {
                return true;
            }

            _cookies = new CookieContainer();

            var initialResponse = MakeRequest(LogonLandingUrl, "GET", null);
            var galx = string.Empty;
            using (var reader = new StreamReader(initialResponse.GetResponseStream()))
            {
                var response = reader.ReadToEnd();
                galx = ExtractText(response, GALXExpression);
            }

            var data = string.Format("Email={0}&Passwd={1}&GALX={2}&rmShown=1&service=grandcentral", 
                HttpUtility.UrlEncode(_user), HttpUtility.UrlEncode(_password), galx);
            var logonResponse = MakeRequest(LogonPostUrl, "POST", data);
            if (logonResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(string.Format("Could not login: {0}", logonResponse.StatusCode));
            }

            _loggedin = true;

            try
            {
                // Make sure we authenticated correctly
                GetInboxCalls();

                var inboxResponse = MakeRequest(MainUrl, "GET", null);
                if (inboxResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(string.Format("Could not load Inbox: {0}", inboxResponse.StatusCode));
                }

                using (StreamReader inboxReader = new StreamReader(inboxResponse.GetResponseStream()))
                {
                    var inbox = inboxReader.ReadToEnd();

                    _rnr = ExtractText(inbox, RNRExpression);
                }
            }
            catch(Exception inner)
            {
                Logout();
                throw new Exception("Could not load Inbox", inner);
            }

            return _loggedin;
        }

        private string ExtractText(string text, Regex expression)
        {
            var match = expression.Match(text);
            return match.Groups[1].Value;
        }

        private XDocument GetData(string url)
        {
            EnsureLoggedIn();

            var response = MakeRequest(url, "GET", null);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(string.Format("Could not load {0}: {0}", url, response.StatusCode));
            }

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var xml = reader.ReadToEnd();
                var document = new XmlDocument();
                document.LoadXml(xml);

                var json = document.SelectSingleNode("/response/json").FirstChild.InnerText;
                var jsonStream = new MemoryStream(UTF8Encoding.Default.GetBytes(json));
                return ParseJson(jsonStream);
            }
        }

        private void EnsureLoggedIn()
        {
            if (!Login())
            {
                throw new Exception("Could not log into Google Voice account");
            }
        }

        static private string SanitizeNumber(string number)
        {
            var result = new StringBuilder();
            foreach (var c in number)
            {
                if (char.IsDigit(c))
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        HttpWebResponse MakeRequest(string url, string method, string data)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = _cookies;
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = method;
            request.UserAgent = "GVDialer/1.0";
            request.ContentType = "application/x-www-form-urlencoded";

            var cookieHeader = _cookies.GetCookieHeader(CookieUri);
            if (cookieHeader != string.Empty)
            {
                request.Headers.Add(HttpRequestHeader.Cookie, cookieHeader);
            }

            if (data != null)
            {
                var dataBuffer = Encoding.ASCII.GetBytes(data);
                request.ContentLength = dataBuffer.Length;

                var requestStream = request.GetRequestStream();
                requestStream.Write(dataBuffer, 0, dataBuffer.Length);
                requestStream.Close();
            }

            return (HttpWebResponse)request.GetResponse();
        }

        public bool Logout()
        {
            if (_loggedin)
            {
                var response = MakeRequest(LogoutUrl, "GET", null);
                var reader = new StreamReader(response.GetResponseStream());
                var text = reader.ReadToEnd();

                _cookies = null;
                _loggedin = false;
            }
            return true;
        }

        public void Dial(string dest, string source)
        {
            EnsureLoggedIn();

            var data = string.Format("outgoingNumber={0}&forwardingNumber={1}&_rnr_se={2}&phoneType=2",
                HttpUtility.UrlEncode(SanitizeNumber(dest)), HttpUtility.UrlEncode(SanitizeNumber(source)), HttpUtility.UrlEncode(_rnr));
            var response = MakeRequest(CallUrl, "POST", data);
            VerifyCommand(response);
        }

        public void SMS(string dest, string message)
        {
            EnsureLoggedIn();

            var count = (int)Math.Ceiling(message.Length / (double)SMSLength);
            for (var i = 0; i < count; ++i)
            {
                var offset = i * SMSLength;
                var length = Math.Min(SMSLength, message.Length - offset);
                var splitMessage = message.Substring(offset, length);

                var data = string.Format("phoneNumber={0}&text={1}&_rnr_se={2}", 
                    HttpUtility.UrlEncode(SanitizeNumber(dest)), HttpUtility.UrlEncode(splitMessage), HttpUtility.UrlEncode(_rnr));
                var response = MakeRequest(SMSUrl, "POST", data);
                VerifyCommand(response);
            }
        }

        XDocument VerifyCommand(HttpWebResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(string.Format("Bad response: {0}", response.StatusCode));
            }

            var result = ParseJson(response.GetResponseStream());
            if (!(bool)result.Root.Element("ok"))
            {
                throw new Exception(
                    string.Format("Bad status, code {0}",
                        (int)result.Root.Element("data").Element("code")));
            }
            return result;
        }

        XDocument ParseJson(Stream stream)
        {
            using (var reader = JsonReaderWriterFactory.CreateJsonReader(stream, XmlDictionaryReaderQuotas.Max))
            {
                var result = XDocument.Load(reader);
                return result;
            }
        }

        public XDocument GetPlacedCalls()
        {
            return GetData(PlacedCallsUrl);
        }

        public XDocument GetReceivedCalls()
        {
            return GetData(ReceivedCallsUrl);
        }

        public XDocument GetMissedCalls()
        {
            return GetData(MissedCallsUrl);
        }

        public XDocument GetAllCalls()
        {
            return GetData(AllCallsUrl);
        }

        public XDocument GetVoicemails()
        {
            return GetData(VoicemailUrl);
        }

        public XDocument GetSMSMessages()
        {
            return GetData(SMSMessagesUrl);
        }

        public XDocument GetRecordedCalls()
        {
            return GetData(RecordedCallsUrl);
        }

        public XDocument GetStarredCalls()
        {
            return GetData(StarredCallsUrl);
        }

        public XDocument GetInboxCalls()
        {
            return GetData(InboxUrl);
        }

        public XDocument GetSpamCalls()
        {
            return GetData(SpamUrl);
        }

        public XDocument GetTrashCalls()
        {
            return GetData(TrashUrl);
        }

        public XDocument GetContacts()
        {
            return GetData(ContactsUrl);
        }

        public void GetInboxUrl(out string url, out byte[] data, out string headers)
        {
            url = LogonPostUrl;

            var postData = string.Format("voice&Email={0}&Passwd={1}",
                HttpUtility.UrlEncode(_user), HttpUtility.UrlEncode(_password));
            data = Encoding.ASCII.GetBytes(postData);

            headers = "Content-Type: application/x-www-form-urlencoded\r\n";
        }
    }
}
