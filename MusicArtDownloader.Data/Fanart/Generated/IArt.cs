using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Data.Fanart.Generated
{
    public interface IArt
    {
        /// <summary>
        /// Gets or sets the Fanart.tv ID of the image.
        /// </summary>
        int id { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image.
        /// </summary>
        string url { get; set; }

        /// <summary>
        /// Gets or sets the number of likes of the image.
        /// </summary>
        int likes { get; set; }
    }
}
