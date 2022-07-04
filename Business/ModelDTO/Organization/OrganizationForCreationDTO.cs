using Domains;
using System;

namespace Business.ModelDTO
{
    public class OrganizationForCreationDTO
    {
        public string Name { get; set; }
        public Guid RegionId { get; set; }
        public RegionDTO Region { get; set; }
        public string MessageTitle { get; set; }
        public string ContactNumber { get; set; }
    }
}
