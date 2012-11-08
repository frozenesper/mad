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
        private readonly Dictionary<string, string> requestResponses;
        private readonly Dictionary<string, int> requestMatches;
        private int requests = 0;

        public Dictionary<string, int> MatchingRequests { get { return this.requestMatches; } }
        public int Requests { get { return this.requests; } }

        public FakeHandler(string uriContains, string response)
            : this()
        {
            this.requestResponses = new Dictionary<string, string>();
            this.requestResponses.Add(uriContains, response);
        }

        public FakeHandler(Dictionary<string, string> requestResponses)
            : this()
        {
            this.requestResponses = requestResponses;
        }

        private FakeHandler()
        {
            this.InnerHandler = new HttpClientHandler();
            this.requestMatches = new Dictionary<string, int>();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            this.requests++;
            var match = this.RequestsContains(request.RequestUri.ToString());
            if (!String.IsNullOrEmpty(match))
            {
                this.IncrementMatch(match);
                var r = new HttpResponseMessage();
                r.StatusCode = System.Net.HttpStatusCode.OK;
                r.Version = new Version("1.1");
                r.Content = new StringContent(this.requestResponses[match]);
                r.RequestMessage = request;
                
                return Task.Factory.StartNew(() => r);
            }

            throw new ArgumentException("uriContains");
            //return base.SendAsync(request, cancellationToken);
        }

        private void IncrementMatch(string match)
        {
            int value;
            if (!this.requestMatches.TryGetValue(match, out value))
                value = 0;

            this.requestMatches[match] = value + 1;
        }

        private string RequestsContains(string request)
        {
            return this.requestResponses.FirstOrDefault(p =>
                request.Contains(p.Key)).Key;
        }
    }
}
