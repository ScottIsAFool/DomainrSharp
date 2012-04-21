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
    public class Registrar
    {
        [DataMember(Name = "registrar")]
        public string RegistrarDomain { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "register_url")]
        public string RegisterUrl { get; set; }
    }
}
