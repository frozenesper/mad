using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Common
{
    [System.Diagnostics.DebuggerDisplay("{Id}")]
    public class Album : IEquatable<Album>
    {
        /// <summary>
        /// Gets or sets the name of the album.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ID of the album. Most likely the MusicBrainz ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a collection of cdARTs for the album.
        /// </summary>
        public IReadOnlyCollection<CdArt> CdArts { get; set; }

        /// <summary>
        /// Gets or sets a collection of cover art for the album.
        /// </summary>
        public IReadOnlyCollection<Art> Covers { get; set; }

        #region Equality

        public bool Equals(Album o)
        {
            return o != null &&
                   this.Id == o.Id &&
                   this.Name == o.Name &&
                   this.Covers.SequenceEqual(o.Covers) &&
                   this.CdArts.SequenceEqual(o.CdArts);
        }

        public override bool Equals(object o)
        {
            return o is Album &&
                   this.Equals((Album)o);                   
        }

        public override int GetHashCode()
        {
            int hash = 33;
            hash = (hash * 7) + this.Id.GetHashCode();
            hash = (hash * 7) + this.Name.GetHashCode();
            hash = (hash * 7) + this.Covers.GetHashCode();
            hash = (hash * 7) + this.CdArts.GetHashCode();
            return hash;
        }

        #endregion
    }
}
