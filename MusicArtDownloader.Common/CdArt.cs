﻿using System;
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
    }
}
