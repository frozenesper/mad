using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Common
{
    [System.Diagnostics.DebuggerDisplay("{Name} ({Id})")]
    public class Artist : IEquatable<Artist>
    {
        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ID of the artists. Most likely a MusicBrainz ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the date the artist was retrieved.
        /// </summary>
        public DateTime? Retrieved { get; set; }

        /// <summary>
        /// Gets or sets a collection of backgrounds for the artist.
        /// </summary>
        public IReadOnlyCollection<Art> Backgrounds { get; set; }

        /// <summary>
        /// Gets or sets a collection of albums for the artist.
        /// </summary>
        public IReadOnlyCollection<Album> Albums { get; set; }

        /// <summary>
        /// Gets or sets a collection of thumbnails for the artist.
        /// </summary>
        public IReadOnlyCollection<Art> Thumbs { get; set; }

        /// <summary>
        /// Gets or sets a collection of ClearLOGOs for the artist.
        /// </summary>
        public IReadOnlyCollection<Art> ClearLogos { get; set; }

        /// <summary>
        /// Gets or sets a collection of High-Definition ClearLOGOs for the artist.
        /// </summary>
        public IReadOnlyCollection<Art> HdClearLogo { get; set; }

        /// <summary>
        /// Gets or sets a collection of banners for the artist.
        /// </summary>
        public IReadOnlyCollection<Art> Banners { get; set; }

        #region Equality

        public bool Equals(Artist o)
        {
            return o != null &&
                   this.Id == o.Id &&
                   this.Name == o.Name &&
                   this.Retrieved == o.Retrieved &&
                   this.Backgrounds.SequenceEqual(o.Backgrounds) &&
                   this.Albums.SequenceEqual(o.Albums) &&
                   this.Thumbs.SequenceEqual(o.Thumbs) &&
                   this.ClearLogos.SequenceEqual(o.ClearLogos) &&
                   this.HdClearLogo.SequenceEqual(o.HdClearLogo) &&
                   this.Banners.SequenceEqual(o.Banners);
        }

        public override bool Equals(object o)
        {
            return o is Artist &&
                   this.Equals((Artist)o);
        }

        public override int GetHashCode()
        {
            int hash = 33;
            hash = (hash * 7) + this.Id.GetHashCode();
            hash = (hash * 7) + this.Name.GetHashCode();
            hash = (hash * 7) + this.Retrieved.GetHashCode();
            hash = (hash * 7) + this.Backgrounds.GetHashCode();
            hash = (hash * 7) + this.Albums.GetHashCode();
            hash = (hash * 7) + this.Thumbs.GetHashCode();
            hash = (hash * 7) + this.ClearLogos.GetHashCode();
            hash = (hash * 7) + this.HdClearLogo.GetHashCode();
            hash = (hash * 7) + this.Banners.GetHashCode();
            return hash;
        }

        #endregion
    }
}
