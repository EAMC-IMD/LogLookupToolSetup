using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;

namespace SapphTools.Utils.UX {
    internal class ADHelper {
        #region fields
        private Hashtable _htChildren = new Hashtable();
        private Dictionary<string, string> _children = new Dictionary<string, string>();
        private DirectoryEntry entry;
        #endregion

        #region properties
        public Dictionary<string, string> Children {
            get { return _children; }
        }
        #endregion

        #region constructors
        public ADHelper() {}
        public void GetChildEntries() {
            GetChildEntries("", false);
        }
        public void GetChildEntries(string adspath) {
            GetChildEntries(adspath, false);
        }
        #endregion

        #region methods
        public void GetChildEntries(string adspath, bool ouOnly) {
            if (adspath.Length > 0)
                entry = new DirectoryEntry(adspath);
            else
                entry = new DirectoryEntry();
            foreach (DirectoryEntry childEntry in entry.Children) {
                if (ouOnly && childEntry.SchemaClassName == "container")
                    _children.Add(childEntry.Name, childEntry.Path);
            }
        }
        #endregion
    }
}
