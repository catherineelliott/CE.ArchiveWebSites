using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.ArchiveWebSites.Core.Models
{
    public class ApiResponse<T>
    {
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; set; }

        public T ResponseData { get; set; }
    }
}
