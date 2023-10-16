using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace SapphTools.Utils.UX {
    internal class ADHelper {
        #region fields
        private List<DirectoryEntry> _children = new List<DirectoryEntry>();
        private DirectoryEntry entry;
        #endregion

        #region properties
        public List<DirectoryEntry> Children {
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
            if (ouOnly)
                _children = entry.Children
                    .OfType<DirectoryEntry>()
                    .Where(s => s.SchemaClassName == "organizationalUnit")
                    .ToList();
            else
                _children = entry.Children
                    .OfType<DirectoryEntry>()
                    .ToList();
        }
        public void GetChildEntriesForEach(string adspath, bool ouOnly) {
            if (adspath.Length > 0)
                entry = new DirectoryEntry(adspath);
            else
                entry = new DirectoryEntry();
            foreach (DirectoryEntry childEntry in entry.Children) {
                if (ouOnly && childEntry.SchemaClassName == "organizationalUnit")
                    _children.Add(childEntry);
                else if (!ouOnly)
                    _children.Add(childEntry);
            }
        }
        #endregion
    }
}
