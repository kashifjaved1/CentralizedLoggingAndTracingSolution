using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Entities
{
    public class Request : DefaultEntity
    {
        public string ServiceName { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
    }

}
