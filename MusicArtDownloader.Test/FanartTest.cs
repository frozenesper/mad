using MusicArtDownloader.Common;
using MusicArtDownloader.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace MusicArtDownloader.Test
{
    public class FanartTest
    {
        [Fact]
        public void CanGetRadioheadByMusicBrainzId()
        {
            const string id = "a74b1b7f-71a5-4011-9441-d0b5e4122711";
            var xml = File.ReadAllText("radiohead.xml");
            var handler = new FakeHandler(id, xml);
            var client = new HttpClient(handler);
            using (var ctx = new FanartContext(client, true))
            {
                var artist = ctx.Music.GetArtistByMusicBrainzId(id);
                Assert.Equal(artist.Name, "Radiohead");
            }
        }

        [Theory]
        [InlineData("radiohead.xml")]
        [InlineData("discovery-lp.xml")]
        public void CanDeserializeAndSerializeFanartMusicApiCalls(string path)
        {
            var xml = File.ReadAllText(path);
            using (var fanart = new FanartContext())
            {
                var music = fanart.Music;

                var artist = music.GetArtistFromXml(xml);
                var roundtrip = music.GetXmlFromArtist(artist);
                var artist2 = music.GetArtistFromXml(roundtrip);
                var roundtrip2 = music.GetXmlFromArtist(artist2);

                Assert.Equal(roundtrip, roundtrip2);
            }
        }

        [Theory]
        [InlineData("radiohead.xml", "discovery-lp.xml")]
        public void CanCombineAndSeperateArtists(string path1, string path2)
        {
            using (var fanart = new FanartContext())
            {
                var music = fanart.Music;
                var paths = new string[] { path1, path2 };

                var artists = paths.Select(p => File.ReadAllText(p))
                                   .Select(x => music.GetArtistFromXml(x))
                                   .ToList();

                var xml = music.GetXmlFromArtists(artists);
                var roundtrip = music.GetArtistsFromXml(xml).ToList();

                Assert.Equal(artists, roundtrip);
            }
        }
    }
}
