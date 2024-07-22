using Core.Data.Entities.Base;

namespace Core.Data.Entities
{
    public class DefaultEntity : BaseEntity
    {
        public Guid RequestId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
