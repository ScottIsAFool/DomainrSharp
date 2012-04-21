using System;
using System.Net;
#if (WINRT || NET45)
using System.Net.Http;
#endif
using Newtonsoft.Json;

#if (SILVERLIGHT && !WINDOWS_PHONE)
namespace DomainrSharp.Silverlight
#elif WINDOWS_PHONE
namespace DomainrSharp.WindowsPhone
#elif WINRT
namespace DomainrSharp.WinRT
#else
namespace DomainrSharp
#endif
{
    public class DomainrSharpService
    {
        private static string QueryUrl = "http://domai.nr/api/json/search?q={0}";
        private static string InfoUrl = "http://domai.nr/api/json/info?q={0}";

        public delegate void SearchResultHandler(object sender, SearchResultsEventsArgs e);
        public delegate void DomainrInfoHandler(object sender, DomainrInfoEventArgs e);

        public event SearchResultHandler SearchCompleted;
        public event DomainrInfoHandler InfoDownloadCompleted;

        public DomainrSharpService()
        {

        }

#if (!SILVERLIGHT && !WINRT)
        /// <summary>
        /// Searches the specified search term.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        public SearchResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                throw new NullReferenceException("Search term cannot be empty");

            SearchResult result = null;
            ZippedClient client = new ZippedClient();
            string url = string.Format(QueryUrl, searchTerm);

            string json = client.DownloadString(url);
            if (!string.IsNullOrEmpty(json))
            {
                result = JsonConvert.DeserializeObject<SearchResult>(json);
            }

            return result;
        }
#endif

        /// <summary>
        /// Does the search asynchronously
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        public void SearchAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                throw new NullReferenceException("Search term cannot be empty");

#if (WINRT || NET45)
            string url = string.Format(QueryUrl, searchTerm);

            HttpClient client = new HttpClient();
            client.GetAsync(url).ContinueWith((requestTask) =>
            {
                HttpResponseMessage response = requestTask.Result;
                response.EnsureSuccessStatusCode();

                response.Content.ReadAsStringAsync().ContinueWith(readTask =>
                {
                    ParseSearchResultString(readTask.Result);
                });
            });
#else
#if (!SILVERLIGHT && !WINRT)
            ZippedClient client = new ZippedClient();
#else
            WebClient client = new SharpGIS.GZipWebClient();
#endif
            
            client.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error == null)
                {
                    ParseSearchResultString(e.Result);
                }
                else
                {
                    if (SearchCompleted != null)
                        SearchCompleted(this, new SearchResultsEventsArgs(e.Error));
                }
            };
            string url = string.Format(QueryUrl, searchTerm);
            client.DownloadStringAsync(new Uri(url, UriKind.Absolute));
#endif
        }

        private void ParseSearchResultString(string json)
        {
            if (json != null)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<SearchResult>(json);
                    if (result != null)
                    {
                        if (SearchCompleted != null)
                            SearchCompleted(this, new SearchResultsEventsArgs(result));
                    }
                    else
                    {
                        if (SearchCompleted != null)
                            SearchCompleted(this, new SearchResultsEventsArgs(new SearchResult()) { Result = null });
                    }
                }
                catch (Exception ex)
                {
                    if (SearchCompleted != null)
                        SearchCompleted(this, new SearchResultsEventsArgs(ex));
                }
            }
            else
            {
                if (SearchCompleted != null)
                    SearchCompleted(this, new SearchResultsEventsArgs(new SearchResult()) { Result = null });
            }
        }

        private void ParseInfoDownloadString(string json)
        {
            if (json != null)
            {
                try
                {
                    var domainrInfo = JsonConvert.DeserializeObject<DomainrInfo>(json);
                    if (domainrInfo != null)
                    {
                        if (InfoDownloadCompleted != null)
                            InfoDownloadCompleted(this, new DomainrInfoEventArgs(domainrInfo));
                    }
                    else
                    {
                        if (InfoDownloadCompleted != null)
                            InfoDownloadCompleted(this, new DomainrInfoEventArgs(new DomainrInfo()) { Result = null });
                    }
                }
                catch (Exception ex)
                {
                    if (InfoDownloadCompleted != null)
                        InfoDownloadCompleted(this, new DomainrInfoEventArgs(ex));
                }
            }
            else
            {
                if (InfoDownloadCompleted != null)
                    InfoDownloadCompleted(this, new DomainrInfoEventArgs(new DomainrInfo()) { Result = null });
            }
        }

        public void InfoDownloadAsync(string domain)
        {
            if (string.IsNullOrEmpty(domain))
                throw new NullReferenceException("Domain cannot be empty");

#if (WINRT || NET45)
            string url = string.Format(InfoUrl, domain);

            HttpClient client = new HttpClient();
            client.GetAsync(url).ContinueWith((requestTask) =>
            {
                HttpResponseMessage response = requestTask.Result;
                response.EnsureSuccessStatusCode();

                response.Content.ReadAsStringAsync().ContinueWith(readTask =>
                {
                    ParseInfoDownloadString(readTask.Result);
                });
            });
#else
#if (!SILVERLIGHT && !WINRT)
            ZippedClient client = new ZippedClient();
#else
            WebClient client = new SharpGIS.GZipWebClient();
#endif
            client.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error == null)
                {
                    ParseInfoDownloadString(e.Result);
                }
                else
                {
                    if (InfoDownloadCompleted != null)
                        InfoDownloadCompleted(this, new DomainrInfoEventArgs(e.Error));
                }
            };
            string url = string.Format(InfoUrl, domain);
            client.DownloadStringAsync(new Uri(url, UriKind.Absolute));
#endif
        }

#if (!SILVERLIGHT && !WINRT)
        public DomainrInfo InfoDownload(string domain)
        {
            if (string.IsNullOrEmpty(domain))
                throw new NullReferenceException("Domain cannot be empty");

            DomainrInfo result = null;

            ZippedClient client = new ZippedClient();
            string url = string.Format(InfoUrl, domain);

            string json = client.DownloadString(url);

            if (!string.IsNullOrEmpty(json))
            {
                result = JsonConvert.DeserializeObject<DomainrInfo>(json);
            }

            return result;
        }
#endif
    }

#if (!SILVERLIGHT && !WINRT)
    internal class ZippedClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return request;
        }
    }
#endif
}
