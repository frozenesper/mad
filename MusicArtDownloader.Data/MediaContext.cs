using MusicArtDownloader.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Data
{
    public class MediaContext : IDisposable
    {

        /// <summary>
        /// Initializes a new instance of the FileSystemContext class.
        /// </summary>
        public MediaContext()
        {
        }

        public Folder FindAllMedia(string path)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("path");

            // TODO finish reading media (use taglib-sharp or id3.net?)
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
