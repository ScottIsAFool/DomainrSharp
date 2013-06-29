using System.Collections.Generic;
using System.Runtime.Serialization;
using PropertyChanged;

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
    [DataContract, ImplementPropertyChanged]
    public class SearchResult
    {
        [DataMember(Name = "query")]
        public string Query { get; set; }
        [DataMember(Name = "results")]
        public List<Result> Results { get; set; }
    }
}
