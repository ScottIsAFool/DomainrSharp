using System.Collections.Generic;
using System.Runtime.Serialization;

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
    [DataContract]
    public class DomainrInfo
    {
        [DataMember(Name = "query")]
        public string Query { get; set; }
        [DataMember(Name = "domain")]
        public string Domain { get; set; }
        [DataMember(Name = "domain_idna")]
        public string DomainIdna { get; set; }
        [DataMember(Name = "host")]
        public string Host { get; set; }
        [DataMember(Name = "subdomain")]
        public string Subdomain { get; set; }
        [DataMember(Name = "path")]
        public string Path { get; set; }
        [DataMember(Name = "availability")]
        public string Availability { get; set; }
        [DataMember(Name = "tld")]
        public Tld Tld { get; set; }
        [DataMember(Name = "subdomains")]
        public List<object> Subdomains { get; set; }
        [DataMember(Name = "subregistration_permitted")]
        public bool SubregistrationPermitted { get; set; }
        [DataMember(Name = "www_url")]
        public string WWWUrl { get; set; }
        [DataMember(Name = "whois_url")]
        public string WhoisUrl { get; set; }
        [DataMember(Name = "register_url")]
        public string RegisterUrl { get; set; }
        [DataMember(Name = "registrars")]
        public List<Registrar> Registrars { get; set; }
    }
}
