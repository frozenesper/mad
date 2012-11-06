using MusicArtDownloader.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MusicArtDownloader.Test
{
    public class FanartTest
    {
        [Fact]
        public void CanGetRadioheadByMusicBrainzId()
        {
            const string id = "a74b1b7f-71a5-4011-9441-d0b5e4122711";
            var ctx = FanartContext.Instance;
            var artists = ctx.Music.GetArtistByMusicBrainzId(id);
        }
    }
}
