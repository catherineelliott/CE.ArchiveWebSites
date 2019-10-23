using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.ArchiveWebSites.Core.Models
{
    public class Link
    {
        public string Href { get; set; }
        /// <summary>
        /// The nature/outcome of the operation. Self gets the 
        /// actual individual result.
        /// </summary>
        public string Rel { get; set; }
        /// <summary>
        /// The http method used to invoke the call.
        /// </summary>
        public string Method { get; set; }
    }
}
