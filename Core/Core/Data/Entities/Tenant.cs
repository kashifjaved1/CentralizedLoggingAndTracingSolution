using Core.Data.Entities.Base;

namespace Core.Data.Entities
{
    public class Tenant : BaseEntity
    {
        public string Name { get; set; }
        public string Identifier { get; set; }
    }
}
