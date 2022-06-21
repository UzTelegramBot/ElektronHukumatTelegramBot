using System;
using System.Collections.Generic;

namespace Domains
{
    public class Organization : BaseEntity
    {
       public string Name { get; set; }
       public Guid RegionId { get; set; }
       public Region Region { get; set; }
       public string MessageTitle { get; set; }
       ICollection<Manager> Managers { get; set; } 
    }
}
