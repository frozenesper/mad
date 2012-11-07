using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Common
{
    public class CdArt : Art
    {
        /// <summary>
        /// Gets or sets the disc number of the art.
        /// </summary>
        public int Disc { get; set; }

        /// <summary>
        /// Gets or sets the size in pixels of the art.
        /// </summary>
        public int Size { get; set; }

        #region Equality

        public bool Equals(CdArt o)
        {
            return o != null &&
                   base.Equals((Art)o) &&
                   this.Disc == o.Disc &&
                   this.Size == o.Size;
        }

        public override bool Equals(object o)
        {
            return o is CdArt &&
                   this.Equals((CdArt)o);
        }

        public override int GetHashCode()
        {
            int hash = 33;
            hash = (hash * 7) + base.GetHashCode();
            hash = (hash * 7) + this.Disc.GetHashCode();
            hash = (hash * 7) + this.Size.GetHashCode();
            return hash;
        }

        #endregion
    }
}
