using System;
using System.Collections;
using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;

namespace SapphTools.Utils.UX {
    public partial class ADPicker : UserControl {
        private string _adspath = "";
        private bool _ouOnly = false;
        private readonly ArrayList alExceptions = new ArrayList(2);
        private ADHelper adh;
        private string _siteCode;
        public bool OUOnly {
            get => _ouOnly;
            set => _ouOnly = value;
        }
        public string ADsPath {
            get { return _adspath; }
        }
        public string SiteCode {
            get => _siteCode;
            set => _siteCode = value;
        }
        public ADPicker() : this("", false) {}
        public ADPicker(bool ouOnly) : this("", ouOnly) {}
        public ADPicker(string sitecode, bool ouOnly) {
            _ouOnly = ouOnly;
            _siteCode = sitecode;
            InitializeComponent();

            alExceptions.Add("OU=Domain Controllers");
            alExceptions.Add("CN=Computers");
            alExceptions.Add("OU=Computers");
        }
        private void ADPicker_Load(object sender, EventArgs e) {
            TreeNode parentNode = new TreeNode($"LDAP://OU={_siteCode},OU=DoD,DC=med,DC=ds,DC=osd,DC=mil") {
                Tag = $"LDAP://OU={_siteCode},OU=DoD,DC=med,DC=ds,DC=osd,DC=mil"
            };
            treeView1.Nodes.Add(parentNode);
            AddTreeNodes(parentNode);
            treeView1.Nodes[0].Expand();
        }
        private void AddTreeNodes(TreeNode node) {
            Cursor.Current = Cursors.WaitCursor;
            treeView1.BeginUpdate();
            adh = new ADHelper();
            adh.GetChildEntries((string)node.Tag, _ouOnly);
            foreach (var child in adh.Children) {
                TreeNode childNode = new TreeNode(child.Name) {
                    Tag = child.Path,
                    Name = child.Path
                };
                node.Nodes.Add(childNode);
                if (!alExceptions.Contains(node.Text))
                    childNode.ImageIndex =
                        SetImageIndex(child.Name.Substring(0, 2));
                else
                    childNode.ImageIndex = 3;
            }
            treeView1.EndUpdate();
            Cursor.Current = Cursors.Default;
        }
        private void TreeView1_AfterExpand(object sender, TreeViewEventArgs e) {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            foreach (TreeNode childNode in e.Node.Nodes)
                AddTreeNodes(childNode);
            watch.Stop();
            System.Diagnostics.Debug.WriteLine($"Tree expansion, foreach, {e.Node.Tag} :: {e.Node.Text} " + watch.ElapsedMilliseconds.ToString() + "ms");
        }
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e) {
            _adspath = (string)e.Node.Tag;
            if (e.Node.Parent != null) {
                if (!alExceptions.Contains(e.Node.Parent.Text) && e.Node.Parent.Text != "")
                    e.Node.SelectedImageIndex = SetImageIndex(e.Node.Text.Substring(0, 2));
                else
                    e.Node.SelectedImageIndex = 3;
            }
        }
        private int SetImageIndex(string objectType) {
            switch (objectType) {
                case "CN":
                    return 1;
                case "OU":
                    return 2;
                case "DC":
                    return 3;
                default:
                    return 0;
            }
        }
    }
}
