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
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(34, 770);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(133, 42);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // MessageLabel
            // 
            this.MessageLabel.Location = new System.Drawing.Point(12, 9);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(526, 23);
            this.MessageLabel.TabIndex = 1;
            // 
            // OUPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 824);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.OkButton);
            this.Name = "OUPicker";
            this.Text = "OUPicker";
            this.Load += new System.EventHandler(this.OUPicker_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label MessageLabel;
    }
}