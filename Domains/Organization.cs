using System;

namespace Domains
{
    public class Organization : BaseEntity
    {
       public string Name { get; set; }
       public Guid RegionId { get; set; }
       public Region Region { get; set; }
       public string MessageTitle { get; set; }
    }
}
