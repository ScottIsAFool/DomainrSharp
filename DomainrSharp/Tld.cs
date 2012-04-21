using System.Runtime.Serialization;

#if (SILVERLIGHT && !WINDOWS_PHONE)
namespace DomainrSharp.Silverlight
#elif WINDOWS_PHONE
namespace DomainrSharp.WindowsPhone
#else
namespace DomainrSharp
#endif
{
    [DataContract]
    public class Tld
    {
        [DataMember(Name="domain")]
        public string Domain { get; set; }
        [DataMember(Name = "wikipedia_url")]
        public string WikipediaUrl { get; set; }
        [DataMember(Name = "iana_url")]
        public string IanaUrl { get; set; }
    }
}
