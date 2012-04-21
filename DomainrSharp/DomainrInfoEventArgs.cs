// -----------------------------------------------------------------------
// <copyright file="DomainrInfoEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

#if (SILVERLIGHT && !WINDOWS_PHONE)
namespace DomainrSharp.Silverlight
#elif WINDOWS_PHONE
namespace DomainrSharp.WindowsPhone
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
    public class DomainrInfoEventArgs : EventArgs
    {
        public DomainrInfoEventArgs(DomainrInfo info)
        {
            Result = info;
        }

        public DomainrInfoEventArgs(Exception ex)
        {
            Error = ex;
        }

        public DomainrInfo Result { get; set; }
        public Exception Error { get; set; }
    }
}
