using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Common
{
    [System.Diagnostics.DebuggerDisplay("{Name} ({Id})")]
    public class Album
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
        /// Gets or sets a list of cdARTs for the album.
        /// </summary>
        public List<CdArt> CdArts { get; set; }

        /// <summary>
        /// Gets or sets a list of cover art for the album.
        /// </summary>
        public List<Art> Covers { get; set; }
    }
}
