using System;

namespace Domains
{
    public class ABase : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
    }
}
