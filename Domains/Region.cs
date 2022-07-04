using System.Collections.Generic;

namespace Domains
{
    public class Region : BaseEntity
    {
        public long RegionIndex { get; set; }
        public string UzName { get; set; }
        public virtual ICollection<User> Users { get; set; } 
    }
}
