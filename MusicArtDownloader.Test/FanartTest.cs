﻿using MusicArtDownloader.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace MusicArtDownloader.Test
{
    public class FanartTest
    {
        [Fact(Skip="Shouldn't hit web service on every test run.")]
        public void CanGetRadioheadByMusicBrainzId()
        {
            const string id = "a74b1b7f-71a5-4011-9441-d0b5e4122711";
            var ctx = new FanartContext();
            var artists = ctx.Music.GetArtistByMusicBrainzId(id);
        }

        [Theory]
        [InlineData("radiohead.xml")]
        [InlineData("discovery-lp.xml")]
        public void CanDeserializeAndSerializeFanartMusicApiCalls(string path)
        {
            var xml = File.ReadAllText(path);
            var fanart = new FanartContext();
            var music = fanart.Music;

            var artist = music.GetArtistFromXml(xml);
            var roundtrip = music.GetXmlFromArtist(artist);
            var artist2 = music.GetArtistFromXml(roundtrip);
            var roundtrip2 = music.GetXmlFromArtist(artist2);

            Assert.Equal(roundtrip, roundtrip2);
        }

    }
}
