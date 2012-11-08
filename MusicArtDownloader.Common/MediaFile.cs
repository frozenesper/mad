using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Common
{
    public class MediaFile : Node
    {
        /// <summary>
        /// Gets or sets the Artist of the media file.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the Album of the media file.
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// Gets or sets the Title of the media file.
        /// </summary>
        public string Title { get; set; }

        #region Equality

        public bool Equals(MediaFile o)
        {
            return o != null &&
                   base.Equals((Node)o) &&
                   this.Artist == o.Artist &&
                   this.Album == o.Album &&
                   this.Title == o.Title;
        }

        public override bool Equals(object o)
        {
            return o is MediaFile &&
                   this.Equals((MediaFile)o);
        }

        public override int GetHashCode()
        {
            int hash = 33;
            hash = (hash * 7) + base.GetHashCode();
            hash = (hash * 7) + this.Artist.GetHashCode();
            hash = (hash * 7) + this.Album.GetHashCode();
            hash = (hash * 7) + this.Title.GetHashCode();
            return hash;
        }

        #endregion
    }
}
