using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Data.Fanart.Generated
{
    public interface ICdArt : IArt
    {
        /// <summary>
        /// Gets or sets the disc number of the art.
        /// </summary>
        int disc { get; set; }

        /// <summary>
        /// Gets or sets the size in pixels of the art.
        /// </summary>
        int size { get; set; }
    }
}
