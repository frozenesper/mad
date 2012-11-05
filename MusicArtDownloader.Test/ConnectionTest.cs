using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MusicArtDownloader.Test
{
    public class ConnectionTest
    {
        [Fact]
        public void CanSerializeAndDeserializeArtists()
        {
            // from a call to the Fanart.tv API: http://fanart.tv/webservice/artist/40e6d15298ca41f207bcba86bc048288/a74b1b7f-71a5-4011-9441-d0b5e4122711/json/
            // made on 11/05/2012
            const string artistJson = "{'Discovery':{'mbid_id':'ca4ba6af-cb9b-4877-8242-179063db6be4','albums':{'cf5d716f-463f-4fbe-9ec2-135a8cacf41f':{'albumcover':[{'id':'52320','url':'http://fanart.tv/fanart/music/ca4ba6af-cb9b-4877-8242-179063db6be4/albumcover/52320/lp-50594aed164bb.jpg','likes':'0'}],'cdart':[{'id':'54300','url':'http://fanart.tv/fanart/music/ca4ba6af-cb9b-4877-8242-179063db6be4/cdart/54300/lp-50644a7fcd34e.png','likes':'0','disc':'1','size':'1000'}]}}}}";

        }
    }
}
