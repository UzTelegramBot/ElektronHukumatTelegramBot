using System;
using System.Collections.Generic;

namespace Domains
{
    public class Organization : Base
    {
       public string Name { get; set; }
       public Guid RegionId { get; set; }
       public Region Region { get; set; }
       public string MessageTitle { get; set; }
       public string ContactNumber { get; set; }
       public Guid ParentId { get; set; }
       public virtual ICollection<Manager> Managers { get; set; } 
    }
}
