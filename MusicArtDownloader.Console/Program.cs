using System;
using System.Collections.Generic;
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
            var xml = System.IO.File.ReadAllText(discoveryFileName);
            var fanart = new MusicArtDownloader.Data.FanartContext();
            var music = fanart.Music;

            var artist = music.GetArtistFromXml(xml);
            var roundtrip = music.GetXmlFromArtist(artist);
            var artist2 = music.GetArtistFromXml(roundtrip);
            var roundtrip2 = music.GetXmlFromArtist(artist2);
        }
    }
}
