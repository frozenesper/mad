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

        public async Task<Folder> FindAllFolders(string path)
        {
            var root = new DirectoryInfo(path);

            if (!Directory.Exists(path))
                throw new ArgumentException("path");

            var directories = root.EnumerateDirectories("*", SearchOption.AllDirectories);
            return await ParseNodes(root, directories);
        }

        public async Task<Folder> FindAllMedia(string path)
        {
            var root = new DirectoryInfo(path);

            if (!root.Exists)
                throw new ArgumentException("path");

            var infos = root.EnumerateFiles("*", SearchOption.AllDirectories);
            return await ParseNodes(root, infos);
        }

        private async Task<Folder> ParseNodes(DirectoryInfo root, IEnumerable<FileSystemInfo> infos)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
