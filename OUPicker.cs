using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SapphTools.Utils.UX;

namespace SapphTools.LogLookup.Setup {
    public partial class OUPicker : Form {
        public string SelectedOU;
        public string Message;
        private ADPicker adPicker = new ADPicker(true);
        public OUPicker() {
            InitializeComponent();
            Message = string.Empty;
        }
        public OUPicker(string message) {
            InitializeComponent();
            Message = message;
        }
        private void OUPicker_Load(object sender, EventArgs e) {
            MessageLabel.Text = Message;
        }
        private void OkButton_Click(object sender, EventArgs e) {
            SelectedOU = adPicker.ADsPath;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
