// -----------------------------------------------------------------------
// <copyright file="SearchResultsEventsArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SearchResultsEventsArgs : EventArgs
    {
        public SearchResultsEventsArgs(SearchResult result)
        {
            Result = result;
        }

        public SearchResultsEventsArgs(Exception ex)
        {
            Error = ex;
        }

        public SearchResult Result { get; set; }
        public Exception Error { get; set; }
    }
}
