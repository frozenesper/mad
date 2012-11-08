using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicArtDownloader.Common
{
    public abstract class Node
    {
        /// <summary>
        /// Gets or sets the Name of the item.
        /// </summary>
        public string Name { get; set; }

        #region Equality

        public bool Equals(Node o)
        {
            return o != null &&
                   this.Name == o.Name;
        }

        public override bool Equals(object o)
        {
            return o is Node &&
                   this.Equals((Node)o);
        }

        public override int GetHashCode()
        {
            int hash = 33;
            hash = (hash * 7) + this.Name.GetHashCode();
            return hash;
        }

        #endregion
    }
}
