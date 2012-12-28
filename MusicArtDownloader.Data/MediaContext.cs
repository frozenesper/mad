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
        private readonly Folder root;

        /// <summary>
        /// Initializes a new instance of the FileSystemContext class.
        /// </summary>
        /// <param name="root">Root folder of the media library.</param>
        public MediaContext(string root)
        {
            this.root = new Folder()
            {
                Name = Path.GetDirectoryName(root),
                Path = root
            };
        }

        /// <summary>
        /// Gets the root folder of the media library.
        /// </summary>
        public Folder Root { get { return this.root; } }

        public async Task<Folder> FindAllSubFoldersAsync()
        {
            if (!Directory.Exists(this.root.Path))
                throw new ArgumentException("path");

            var dir = new DirectoryInfo(this.root.Path);
            var directories = await Task.Run(() => dir.EnumerateDirectories("*", SearchOption.AllDirectories));
            return await ParseNodesAsync(this.root, directories);
        }

        public async Task<Folder> FindAllMediaAsync()
        {
            var dir = new DirectoryInfo(this.root.Path);

            if (!dir.Exists)
                throw new ArgumentException("path");

            var files = await Task.Run(() => dir.EnumerateFiles("*", SearchOption.AllDirectories));
            return await ParseNodesAsync(root, files);
        }

        private async Task<Folder> ParseNodesAsync(Folder root, IEnumerable<FileSystemInfo> infos)
        {
            var nodes = await Task<List<FileSystemInfo>>.Run(() => infos.ToList());
            return root;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
