namespace PlanetM_LicenseRequestor
{
    partial class RequestLicenseForm
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
            this.rtMachineInfo = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnGenerateInfo = new System.Windows.Forms.Button();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            this.btnSendViaGMail = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtMachineInfo
            // 
            this.rtMachineInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtMachineInfo.Location = new System.Drawing.Point(0, 0);
            this.rtMachineInfo.Name = "rtMachineInfo";
            this.rtMachineInfo.Size = new System.Drawing.Size(535, 330);
            this.rtMachineInfo.TabIndex = 0;
            this.rtMachineInfo.Text = "";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(535, 330);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtMachineInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(535, 330);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnGenerateInfo);
            this.panel3.Controls.Add(this.btnCopyToClipboard);
            this.panel3.Controls.Add(this.btnSendViaGMail);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 274);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(535, 56);
            this.panel3.TabIndex = 0;
            // 
            // btnGenerateInfo
            // 
            this.btnGenerateInfo.Location = new System.Drawing.Point(18, 16);
            this.btnGenerateInfo.Name = "btnGenerateInfo";
            this.btnGenerateInfo.Size = new System.Drawing.Size(126, 23);
            this.btnGenerateInfo.TabIndex = 2;
            this.btnGenerateInfo.Text = "Generate License Info";
            this.btnGenerateInfo.UseVisualStyleBackColor = true;
            this.btnGenerateInfo.Click += new System.EventHandler(this.btnGenerateInfo_Click);
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCopyToClipboard.Location = new System.Drawing.Point(204, 16);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(126, 23);
            this.btnCopyToClipboard.TabIndex = 3;
            this.btnCopyToClipboard.Text = "Copy To Clipboard";
            this.btnCopyToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // btnSendViaGMail
            // 
            this.btnSendViaGMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendViaGMail.Location = new System.Drawing.Point(373, 16);
            this.btnSendViaGMail.Name = "btnSendViaGMail";
            this.btnSendViaGMail.Size = new System.Drawing.Size(126, 23);
            this.btnSendViaGMail.TabIndex = 4;
            this.btnSendViaGMail.Text = "Send via GMail";
            this.btnSendViaGMail.UseVisualStyleBackColor = true;
            this.btnSendViaGMail.Click += new System.EventHandler(this.btnSendViaGMail_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 354);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Planet License Requestor";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtMachineInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnGenerateInfo;
        private System.Windows.Forms.Button btnCopyToClipboard;
        private System.Windows.Forms.Button btnSendViaGMail;
        private System.Windows.Forms.Panel panel2;
    }
}

