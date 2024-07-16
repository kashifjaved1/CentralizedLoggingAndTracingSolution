using Core.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Entities
{
    public class DefaultEntity : BaseEntity
    {
        public Guid RequestId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
