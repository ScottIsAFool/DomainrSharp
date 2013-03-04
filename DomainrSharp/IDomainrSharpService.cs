using System.Threading.Tasks;

namespace DomainrSharp
{
    public interface IDomainrSharpService
    {
        /// <summary>
        /// Searches the async.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        Task<SearchResult> SearchAsync(string searchTerm);

        /// <summary>
        /// Downloads the information asynchronously
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns></returns>
        Task<DomainrInfo> InfoDownloadAsync(string domain);
    }
}