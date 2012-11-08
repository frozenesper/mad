using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Common
{
    public class Folder : Node
    {
        /// <summary>
        /// Gets or sets a collection of items contained in the folder.
        /// </summary>
        public IReadOnlyCollection<Node> Items { get; set; }

        public IReadOnlyCollection<Folder> Folders
        { get { return this.Items.OfType<Folder>().Cast<Folder>().ToList(); } }

        public IReadOnlyCollection<MediaFile> Files
        { get { return this.Items.OfType<MediaFile>().Cast<MediaFile>().ToList(); } }

        #region Equality

        public bool Equals(Folder o)
        {
            return o != null &&
                   base.Equals((Node)o) &&
                   this.Items.SequenceEqual(o.Items);
        }

        public override bool Equals(object o)
        {
            return o is Folder &&
                   this.Equals((Folder)o);
        }

        public override int GetHashCode()
        {
            int hash = 33;
            hash = (hash * 7) + base.GetHashCode();
            hash = (hash * 7) + this.Items.GetHashCode();
            return hash;
        }

        #endregion
    }
}
