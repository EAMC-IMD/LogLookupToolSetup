namespace SapphTools.LogLookup.Setup {
    partial class OUPicker {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
 
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.OkButton = new System.Windows.Forms.Button();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.adPicker = new SapphTools.Utils.UX.ADPicker();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(26, 626);
            this.OkButton.Margin = new System.Windows.Forms.Padding(2);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 34);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // MessageLabel
            // 
            this.MessageLabel.Location = new System.Drawing.Point(12, 7);
            this.MessageLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(391, 19);
            this.MessageLabel.TabIndex = 1;
            // 
            // adPicker
            // 
            this.adPicker.Location = new System.Drawing.Point(12, 30);
            this.adPicker.Name = "adPicker";
            this.adPicker.OUOnly = true;
            this.adPicker.SiteCode = _siteCode;
            this.adPicker.Size = new System.Drawing.Size(391, 591);
            this.adPicker.TabIndex = 2;
            // 
            // OUPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 670);
            this.Controls.Add(this.adPicker);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.OkButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "OUPicker";
            this.Text = "OUPicker";
            this.Load += new System.EventHandler(this.OUPicker_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label MessageLabel;
        private Utils.UX.ADPicker adPicker;
    }
}