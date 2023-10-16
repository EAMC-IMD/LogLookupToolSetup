using System;
using System.Windows.Forms;

namespace SapphTools.LogLookup.Setup {
    public partial class OUPicker : Form {
        public string SelectedOU;
        public string Message;
        private string _siteCode = "";

        public OUPicker() : this(String.Empty) {}
        public OUPicker(string message)  {
            InitializeComponent();
            MessageLabel.Text = message;
            adPicker.SiteCode = Program.configValues["SiteCode"];
        }
        private void OUPicker_Load(object sender, EventArgs e) {
        }
        private void OkButton_Click(object sender, EventArgs e) {
            SelectedOU = adPicker.ADsPath;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
