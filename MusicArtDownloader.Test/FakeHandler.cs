using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Test
{
    public class FakeHandler : DelegatingHandler
    {
        private readonly string uriContains;
        private readonly HttpResponseMessage response;

        public FakeHandler(string uriContains, string response)
        {
            this.InnerHandler = new HttpClientHandler();
            this.uriContains = uriContains;

            var r = new HttpResponseMessage();
            r.StatusCode = System.Net.HttpStatusCode.OK;
            r.Version = new Version("1.1");
            r.Content = new StringContent(response);
            this.response = r;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (request.RequestUri.ToString().Contains(uriContains))
            {
                this.response.RequestMessage = request;
                return Task.Factory.StartNew(() => this.response);
            }

            throw new ArgumentException("uriContains");
            //return base.SendAsync(request, cancellationToken);
        }
    }
}
