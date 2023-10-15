using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using SapphTools.Utils.UX;
using Tulpep.ActiveDirectoryObjectPicker;
using System.Xml;
using System.Linq;

namespace SapphTools.LogLookup.Setup {
    internal static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string configPath = "";
            Dictionary<string, string> configValues = new Dictionary<string, string>();
            XmlDocument xmlSettings = new XmlDocument();
            TextEntryMessageBox msgBox;

            TextEntryMessageBoxIcon TextIcon = TextEntryMessageBoxIcon.Question;
            int TextMaxLength = 255;
            System.Drawing.Font TextFont = new System.Drawing.Font("Tahoma", 11);
            MessageBoxButtons TextButtons = MessageBoxButtons.OKCancel;

            msgBox = TextEntryMessageBoxManager.CreateMessageBox("Site Code");
            msgBox.Caption = "Site Code";
            msgBox.Text = "What is your site code?";
            msgBox.Icon = TextIcon;
            msgBox.InputMaxLength = 6;
            msgBox.Font = TextFont;
            msgBox.AddButtons(TextButtons);

            configValues.Add("SiteCode", msgBox.Show().ToUpper());

            msgBox = TextEntryMessageBoxManager.CreateMessageBox("SQL Server FQDN");
            msgBox.Caption = "SQL Server FQDN";
            msgBox.Text = "What SQL server will logs be written to?";
            msgBox.Icon = TextIcon;
            msgBox.InputMaxLength = TextMaxLength;
            msgBox.InputWidth = 300;
            msgBox.Font = TextFont;
            msgBox.AddButtons(TextButtons);

            configValues.Add("SQLServer", msgBox.Show().ToUpper());
            configValues.Add("Database", "EUDLogging");
            configValues.Add("ConnectionString", $@"Data Source={configValues["SQLServer"]};Initial Catalog={configValues["Database"]};Authentication=Active Directory Integrated");

            using (OUPicker form = new OUPicker("Select the root OU for your site.")) {
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    configValues.Add("LDAPRoot", form.SelectedOU);
                } else
                    Application.Exit();
            }
            using (OUPicker form = new OUPicker("Select the root OU for your site's workstations.")) {
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    configValues.Add("LDAPWorkstation", form.SelectedOU);
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
                configValues.Add("allowedGroups", String.Join(",", results.ToArray()));
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
                configValues.Add("OUGroups", String.Join(",", results.ToArray()));
            }

            configValues.Add("Initialized", "True");
            OpenFileDialog settingsDialog = new OpenFileDialog() {
                CheckFileExists = true,
                Multiselect = false,
                Title = "Location for LogLookupTool.exe.config",
                Filter = "config files (*.config)|*.config"
            };
            if (settingsDialog.ShowDialog() == DialogResult.OK) {
                configPath = settingsDialog.FileName;
            } else {
                Application.Exit();
            }
            xmlSettings.Load(configPath);
            XmlNodeList settingsNodes = xmlSettings.SelectNodes(@"/configuration/applicationSettings/Log_Lookup_Tool.Properties.Settings");
            foreach (XmlNode settingsNode in settingsNodes[0].ChildNodes) {
                if (configValues.Keys.Contains(settingsNode.Attributes["name"].Value)) {
                    settingsNode.FirstChild.InnerText = configValues[settingsNode.Attributes["name"].Value];
                }
            }
            xmlSettings.Save(configPath);

            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder() {
                DataSource = configValues["SQLServer"],
                InitialCatalog = "master",
                IntegratedSecurity = true,
                TrustServerCertificate = true
            };
            using (SqlConnection conn = new SqlConnection(sb.ConnectionString)) {
                string SQL = "SELECT 1 AS DatabaseManager " +
                    "FROM sys.database_role_members rm " +
                    "JOIN sys.database_principals r " +
                    "ON rm.role_principal_id=r.principal_id " +
                    "JOIN sys.database_principals m " +
                    "ON rm.member_principal_id=m.principal_id " +
                    "WHERE r.name = 'dbmanager' OR r.name = 'db_owner'";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(SQL, conn)) {
                    object result = cmd.ExecuteScalar();
                    if (result == DBNull.Value || (int)result != 1) {
                        MessageBox.Show(
                            $"This user does not have enough permissions to create a database on {configValues["SQLServer"]}.",
                            "Access denied.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop
                        );
                        Application.Exit();
                    }
                }
                ServerConnection svrConn = new ServerConnection(conn);
                Server server = new Server(svrConn);
                try {
                    server.ConnectionContext.Authentication = SqlConnectionInfo.AuthenticationMethod.ActiveDirectoryIntegrated;
                    server.ConnectionContext.TrustServerCertificate = true;
                    server.ConnectionContext.Connect();
                    server.ConnectionContext.ExecuteNonQuery(Properties.Resources.EUDLogging);
                } finally {
                    if (server.ConnectionContext.IsOpen) {
                        server.ConnectionContext.Disconnect();
                    }
                }
                conn.Close();
            }
        }
    }
}
