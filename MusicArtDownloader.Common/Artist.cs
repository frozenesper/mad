using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Common
{
    [System.Diagnostics.DebuggerDisplay("{Name} ({Id})")]
    public class Artist
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
        /// Gets or sets a list of backgrounds for the artist.
        /// </summary>
        public List<Art> Backgrounds { get; set; }

        /// <summary>
        /// Gets or sets a list of albums for the artist.
        /// </summary>
        public List<Album> Albums { get; set; }

        /// <summary>
        /// Gets or sets a list of thumbnails for the artist.
        /// </summary>
        public List<Art> Thumbs { get; set; }

        /// <summary>
        /// Gets or sets a list of ClearLOGOs for the artist.
        /// </summary>
        public List<Art> ClearLogo { get; set; }

        /// <summary>
        /// Gets or sets a list of High-Definition ClearLOGOs for the artist.
        /// </summary>
        public List<Art> HdClearLogo { get; set; }

        /// <summary>
        /// Gets or sets a list of banners for the artist.
        /// </summary>
        public List<Art> Banner { get; set; }
    }
}
