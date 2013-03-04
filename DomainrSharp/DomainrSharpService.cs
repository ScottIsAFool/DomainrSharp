using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AdvancedREI.Net.Http.Compression;
using Newtonsoft.Json;

namespace DomainrSharp
{
    public class DomainrSharpService : IDomainrSharpService
    {
        private const string QueryUrl = "http://domai.nr/api/json/search?q={0}";
        private const string InfoUrl = "http://domai.nr/api/json/info?q={0}";

        public string ClientID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainrSharpService" /> class.
        /// </summary>
        public DomainrSharpService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainrSharpService" /> class.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        public DomainrSharpService(string clientId)
        {
            ClientID = clientId;
        }

        /// <summary>
        /// Searches the async.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        public async Task<SearchResult> SearchAsync(string searchTerm)
        {
            var httpClient = new HttpClient(new CompressedHttpClientHandler());

            var url = string.Format(QueryUrl, searchTerm);
            if (!string.IsNullOrEmpty(ClientID))
                url += "&client_id=" + ClientID;

            var resultString = await httpClient.GetStringAsync(url);

            try
            {
                return JsonConvert.DeserializeObject<SearchResult>(resultString);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Downloads the information asynchronously
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns></returns>
        public async Task<DomainrInfo> InfoDownloadAsync(string domain)
        {
            var handler = new HttpClientHandler {AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate};
            var httpClient = new HttpClient(handler);

            var url = string.Format(InfoUrl, domain);
            if (!string.IsNullOrEmpty(ClientID))
                url += "&client_id=" + ClientID;

            var resultString = await httpClient.GetStringAsync(url);

            try
            {
                return JsonConvert.DeserializeObject<DomainrInfo>(resultString);
            }
            catch
            {
                return null;
            }
        }

    }
}
