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
            var fanart = new FanartContext();
            var music = fanart.Music;
            var paths = new string[] { radioheadFileName, discoveryFileName };

            var artists = paths.Select(p => File.ReadAllText(p))
                               .Select(x => music.GetArtistFromXml(x))
                               .ToList();

            var xml = music.GetXmlFromArtists(artists);
            var roundtrip = music.GetArtistsFromXml(xml).ToList();
            var xml2 = music.GetXmlFromArtists(roundtrip);

            var r1 = artists.Last();
            var r2 = roundtrip.Last();

            var e = artists.SequenceEqual(roundtrip);
        }
    }
}
