﻿namespace GVDialer
{
    partial class AboutDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mMessageLabel = new System.Windows.Forms.Label();
            this.mDetailLink = new System.Windows.Forms.LinkLabel();
            this.mOkButton = new System.Windows.Forms.Button();
            this.mTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mLogoBox = new System.Windows.Forms.PictureBox();
            this.mVersionLabel = new System.Windows.Forms.Label();
            this.mTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mLogoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mMessageLabel
            // 
            this.mMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mMessageLabel.AutoSize = true;
            this.mMessageLabel.Location = new System.Drawing.Point(13, 91);
            this.mMessageLabel.Name = "mMessageLabel";
            this.mMessageLabel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.mMessageLabel.Size = new System.Drawing.Size(233, 23);
            this.mMessageLabel.TabIndex = 2;
            this.mMessageLabel.Text = "For more information, see:";
            // 
            // mDetailLink
            // 
            this.mDetailLink.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mDetailLink.AutoSize = true;
            this.mDetailLink.Location = new System.Drawing.Point(13, 114);
            this.mDetailLink.Name = "mDetailLink";
            this.mDetailLink.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.mDetailLink.Size = new System.Drawing.Size(233, 23);
            this.mDetailLink.TabIndex = 1;
            this.mDetailLink.TabStop = true;
            this.mDetailLink.Text = "http://code.google.com/p/gvdialer/";
            this.mDetailLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HandleLinkClicked);
            // 
            // mOkButton
            // 
            this.mOkButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mOkButton.AutoSize = true;
            this.mOkButton.Location = new System.Drawing.Point(92, 140);
            this.mOkButton.Name = "mOkButton";
            this.mOkButton.Size = new System.Drawing.Size(75, 23);
            this.mOkButton.TabIndex = 0;
            this.mOkButton.Text = "OK";
            this.mOkButton.UseVisualStyleBackColor = true;
            this.mOkButton.Click += new System.EventHandler(this.HandleOkClick);
            // 
            // mTableLayoutPanel
            // 
            this.mTableLayoutPanel.AutoSize = true;
            this.mTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mTableLayoutPanel.ColumnCount = 1;
            this.mTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mTableLayoutPanel.Controls.Add(this.mOkButton, 0, 4);
            this.mTableLayoutPanel.Controls.Add(this.mDetailLink, 0, 3);
            this.mTableLayoutPanel.Controls.Add(this.mMessageLabel, 0, 2);
            this.mTableLayoutPanel.Controls.Add(this.mLogoBox, 0, 0);
            this.mTableLayoutPanel.Controls.Add(this.mVersionLabel, 0, 1);
            this.mTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mTableLayoutPanel.Name = "mTableLayoutPanel";
            this.mTableLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.mTableLayoutPanel.RowCount = 5;
            this.mTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mTableLayoutPanel.Size = new System.Drawing.Size(259, 172);
            this.mTableLayoutPanel.TabIndex = 1;
            // 
            // mLogoBox
            // 
            this.mLogoBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mLogoBox.Image = global::GVDialer.Properties.Resources.SplashImage;
            this.mLogoBox.Location = new System.Drawing.Point(24, 13);
            this.mLogoBox.Name = "mLogoBox";
            this.mLogoBox.Size = new System.Drawing.Size(210, 55);
            this.mLogoBox.TabIndex = 3;
            this.mLogoBox.TabStop = false;
            // 
            // mVersionLabel
            // 
            this.mVersionLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.mVersionLabel.AutoSize = true;
            this.mVersionLabel.Location = new System.Drawing.Point(107, 71);
            this.mVersionLabel.Name = "mVersionLabel";
            this.mVersionLabel.Size = new System.Drawing.Size(45, 13);
            this.mVersionLabel.TabIndex = 4;
            this.mVersionLabel.Text = "Version ";
            // 
            // AboutDialog
            // 
            this.AcceptButton = this.mOkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(259, 172);
            this.Controls.Add(this.mTableLayoutPanel);
            this.Icon = global::GVDialer.Properties.Resources.ActiveIcon;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 700);
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About GVDialer";
            this.mTableLayoutPanel.ResumeLayout(false);
            this.mTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mLogoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mMessageLabel;
        private System.Windows.Forms.Button mOkButton;
        private System.Windows.Forms.LinkLabel mDetailLink;
        private System.Windows.Forms.TableLayoutPanel mTableLayoutPanel;
        private System.Windows.Forms.PictureBox mLogoBox;
        private System.Windows.Forms.Label mVersionLabel;
    }
}
