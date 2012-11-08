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
        const string radioheadFileName = @"C:\Users\sachin\Documents\Visual Studio 2012\Projects\mad\MusicArtDownloader.Test\radiohead.xml";
        const string discoveryFileName = @"C:\Users\sachin\Documents\Visual Studio 2012\Projects\mad\MusicArtDownloader.Test\discovery-lp.xml";

        static void Main(string[] args)
        {
            const string id = "a74b1b7f-71a5-4011-9441-d0b5e4122711";
            var xml = File.ReadAllText(radioheadFileName);
            var handler = new MusicArtDownloader.Test.FakeHandler(id, xml);
            var client = new HttpClient(handler);
            using (var ctx = new FanartContext(client: client, disposeClient: true))
            {
                var artist = ctx.Music.GetArtistByMusicBrainzId(id);
            }
        }
    }
}
