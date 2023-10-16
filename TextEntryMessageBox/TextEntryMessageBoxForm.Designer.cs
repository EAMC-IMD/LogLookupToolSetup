using System.Windows.Forms;

namespace SapphTools.Utils.UX {
    partial class TextEntryMessageBoxForm {
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
        /// the contents of method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            panelIcon = new System.Windows.Forms.Panel();
            imageListIcons = new System.Windows.Forms.ImageList(components);
            buttonToolTip = new System.Windows.Forms.ToolTip(components);
            rtbMessage = new System.Windows.Forms.RichTextBox();
            inputBox = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // panelIcon
            // 
            panelIcon.BackColor = System.Drawing.Color.Transparent;
            panelIcon.Location = new System.Drawing.Point(11, 10);
            panelIcon.Name = "panelIcon";
            panelIcon.Size = new System.Drawing.Size(45, 39);
            panelIcon.TabIndex = 3;
            panelIcon.Visible = false;
            // 
            // imageListIcons
            // 
            imageListIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
            imageListIcons.ImageSize = new System.Drawing.Size(32, 32);
            imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // rtbMessage
            // 
            rtbMessage.BackColor = System.Drawing.SystemColors.Control;
            rtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            rtbMessage.Location = new System.Drawing.Point(280, 10);
            rtbMessage.Name = "rtbMessage";
            rtbMessage.ReadOnly = true;
            rtbMessage.Size = new System.Drawing.Size(140, 58);
            rtbMessage.TabIndex = 4;
            rtbMessage.Text = "";
            rtbMessage.Visible = false;
            // 
            // inputBox
            // 
            inputBox.Location = new System.Drawing.Point(16, 105);
            inputBox.Name = "inputBox";
            inputBox.Size = new System.Drawing.Size(73, 24);
            inputBox.TabIndex = 5;
            inputBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(inputBox_KeyPress);
            // 
            // TextEntryMessageBoxForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(322, 224);
            Controls.Add(inputBox);
            Controls.Add(rtbMessage);
            Controls.Add(panelIcon);
            Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TextEntryMessageBoxForm";
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private TextBox inputBox;
    }
}