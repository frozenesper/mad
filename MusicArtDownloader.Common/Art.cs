using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Common
{
    [System.Diagnostics.DebuggerDisplay("{Id}")]
    public class Art
    {
        /// <summary>
        /// Gets or sets the Fanart.tv ID of the image.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Gets the preview URL of the image.
        /// </summary>
        public string PreviewUrl { get { return this.Url + "/preview"; } }

        /// <summary>
        /// Gets or sets the number of likes of the image.
        /// </summary>
        public int Likes { get; set; }
    }
}
