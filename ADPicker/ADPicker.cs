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
        public bool OUOnly {
            get => _ouOnly;
            set => _ouOnly = value;
        }
        public string ADsPath {
            get { return _adspath; }
        }
        public ADPicker() {
            InitializeComponent();
            alExceptions.Add("OU=Domain Controllers");
            alExceptions.Add("CN=Computers");
            alExceptions.Add("OU=Computers");
            _ouOnly = false;
        }
        public ADPicker(bool ouOnly) {
            _ouOnly = ouOnly;
            InitializeComponent();

            alExceptions.Add("OU=Domain Controllers");
            alExceptions.Add("CN=Computers");
            alExceptions.Add("OU=Computers");
        }
        private void ADPicker_Load(object sender, EventArgs e) {
            TreeNode parentNode = new TreeNode("Root") {
                Tag = ""
            };
            treeView1.Nodes.Add(parentNode);
            AddTreeNodes(parentNode);
            treeView1.Nodes[0].Expand();

            foreach (TreeNode childNode in parentNode.Nodes)
                AddTreeNodes(childNode);
        }

        private void AddTreeNodes(TreeNode node) {
            Cursor.Current = Cursors.WaitCursor;
            treeView1.BeginUpdate();
            adh = new ADHelper();
            adh.GetChildEntries((string)node.Tag, _ouOnly);
            foreach (var child in adh.Children) {
                TreeNode childNode = new TreeNode(child.Key) {
                    Tag = child.Value
                };
                node.Nodes.Add(childNode);
                if (!alExceptions.Contains(node.Text))
                    childNode.ImageIndex =
                        SetImageIndex(child.Key.Substring(0, 2));
                else
                    childNode.ImageIndex = 3;
            }
            treeView1.EndUpdate();
            Cursor.Current = Cursors.Default;
        }
        private void TreeView1_AfterExpand(object sender, TreeViewEventArgs e) {
            foreach (TreeNode childNode in e.Node.Nodes)
                AddTreeNodes(childNode);
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
