using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SapphTools.Utils.UX;
using Tulpep.ActiveDirectoryObjectPicker;

namespace SapphTools.LogLookup.Setup {
    internal static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TextEntryMessageBoxIcon TextIcon = TextEntryMessageBoxIcon.Question;
            int TextMaxLength = 255;
            System.Drawing.Font TextFont = new System.Drawing.Font("Tahoma", 11);
            MessageBoxButtons TextButtons = MessageBoxButtons.OKCancel;

            TextEntryMessageBox msgBox = TextEntryMessageBoxManager.CreateMessageBox("Site Code");
            msgBox.Caption = "Site Code";
            msgBox.Text = "What is your site code?";
            msgBox.Icon = TextIcon;
            msgBox.InputMaxLength = 6;
            msgBox.Font = TextFont;
            msgBox.AddButtons(TextButtons);

            string SiteCode = msgBox.Show().ToUpper();

            msgBox = TextEntryMessageBoxManager.CreateMessageBox("SQL Server FQDN");
            msgBox.Caption = "SQL Server FQDN";
            msgBox.Text = "What SQL server will logs be written to?";
            msgBox.Icon = TextIcon;
            msgBox.InputMaxLength = TextMaxLength;
            msgBox.InputWidth = 300;
            msgBox.Font = TextFont;
            msgBox.AddButtons(TextButtons);

            string SQLServer = msgBox.Show();
            string LDAPRoot = "";
            string LDAPWorkstation = "";
            string allowedGroups;
            string OUGroups;
            using (OUPicker form = new OUPicker("Select the root OU for your site.")) {
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    LDAPRoot = form.SelectedOU;
                } else
                    Application.Exit();
            }
            using (OUPicker form = new OUPicker("Select the root OU for your site's workstations.")) {
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    LDAPRoot = form.SelectedOU;
                } else
                    Application.Exit();
            }
            MessageBox.Show("In the next dialog, select the Security Groups permitted to launch this application.");
            DirectoryObjectPickerDialog picker = new DirectoryObjectPickerDialog() {
                AllowedObjectTypes = ObjectTypes.Groups,
                DefaultObjectTypes = ObjectTypes.Computers,
                AllowedLocations = Locations.All,
                DefaultLocations = Locations.JoinedDomain,
                MultiSelect = true,
                ShowAdvancedView = true
            };
            using (picker) {
                List<string> results = new List<string>();
                if (picker.ShowDialog() == DialogResult.OK) {
                    foreach (var sel in picker.SelectedObjects) {
                        results.Add(sel.Name);
                    }
                }
                allowedGroups = String.Join(",", results.ToArray());
            }
            MessageBox.Show("In the next dialog, select the Security Groups permitted enable and disable objects.");
            picker = new DirectoryObjectPickerDialog() {
                AllowedObjectTypes = ObjectTypes.Groups,
                DefaultObjectTypes = ObjectTypes.Computers,
                AllowedLocations = Locations.All,
                DefaultLocations = Locations.JoinedDomain,
                MultiSelect = true,
                ShowAdvancedView = true
            };
            using (picker) {
                List<string> results = new List<string>();
                if (picker.ShowDialog() == DialogResult.OK) {
                    foreach (var sel in picker.SelectedObjects) {
                        results.Add(sel.Name);
                    }
                }
                OUGroups = String.Join(",", results.ToArray());
            }
            //Application.Run(new TextEntryDialog());
        }
    }
}
