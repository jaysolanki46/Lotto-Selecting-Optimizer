namespace Lotto_Selecting_Optimizer
{
    partial class LottoThankYouPage
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabelShopAgain = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1172, 803);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Lotto New Zealand";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(419, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 69);
            this.label1.TabIndex = 2;
            this.label1.Text = "Good luck !";
            // 
            // linkLabelShopAgain
            // 
            this.linkLabelShopAgain.AutoSize = true;
            this.linkLabelShopAgain.Location = new System.Drawing.Point(536, 292);
            this.linkLabelShopAgain.Name = "linkLabelShopAgain";
            this.linkLabelShopAgain.Size = new System.Drawing.Size(93, 17);
            this.linkLabelShopAgain.TabIndex = 3;
            this.linkLabelShopAgain.TabStop = true;
            this.linkLabelShopAgain.Text = "Shop Again ?";
            this.linkLabelShopAgain.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelShopAgain_LinkClicked);
            // 
            // LottoThankYouPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 803);
            this.Controls.Add(this.linkLabelShopAgain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LottoThankYouPage";
            this.Text = "Lotto New Zealand";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabelShopAgain;
    }
}