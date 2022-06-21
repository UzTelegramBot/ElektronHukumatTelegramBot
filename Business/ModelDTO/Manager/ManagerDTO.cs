using System;

namespace Business.ModelDTO
{
    public class ManagerDTO : ManagerForCreationDTO
    {
        public Guid Id { get; set; }
        public RegionDTO Region { get; set; }
        public OrganizationDTO Organization { get; set; }
    }
}
