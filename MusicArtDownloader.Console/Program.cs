using MusicArtDownloader.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Console
{
    class Program
    {
        const string root = @"\\hda\music\albums";
        const string radioheadFileName = @"C:\Users\sachin\Documents\Visual Studio 2012\Projects\mad\MusicArtDownloader.Test\radiohead.xml";
        const string discoveryFileName = @"C:\Users\sachin\Documents\Visual Studio 2012\Projects\mad\MusicArtDownloader.Test\discovery-lp.xml";

        static void Main(string[] args)
        {
            var xml = File.ReadAllText(radioheadFileName);
            using (var ctx = new MediaContext(root))
            {
                var folder = ctx.FindAllSubFoldersAsync().Result;
            }
        }
    }
}
