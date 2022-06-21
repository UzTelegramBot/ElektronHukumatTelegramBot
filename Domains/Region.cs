using System.Collections.Generic;

namespace Domains
{
    public class Region : BaseEntity
    {
        public int RegionIndex { get; set; }
        public string UzName { get; set; }
        public string RuName { get; set; }
        public string EngName { get; set; }
        public ICollection<User> Users { get; set; } 
    }
}
