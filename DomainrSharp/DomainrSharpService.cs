using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DomainrSharp
{
    public class DomainrSharpService : IDomainrSharpService
    {
        private HttpClient HttpClient { get; set; }

        private const string QueryUrl = "http://domai.nr/api/json/search?q={0}";
        private const string InfoUrl = "http://domai.nr/api/json/info?q={0}";

        public string ClientId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainrSharpService" /> class.
        /// </summary>
        public DomainrSharpService()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainrSharpService" /> class.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        public DomainrSharpService(string clientId)
        {
            ClientId = clientId;
            HttpClient = new HttpClient(new HttpClientHandler{AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip});
        }

        /// <summary>
        /// Searches the async.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        public async Task<SearchResult> SearchAsync(string searchTerm)
        {

            var url = string.Format(QueryUrl, searchTerm);
            if (!string.IsNullOrEmpty(ClientId))
                url += "&client_id=" + ClientId;

            var resultString = await HttpClient.GetStringAsync(url);

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
            var url = string.Format(InfoUrl, domain);
            if (!string.IsNullOrEmpty(ClientId))
                url += "&client_id=" + ClientId;

            var resultString = await HttpClient.GetStringAsync(url);

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
