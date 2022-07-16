using System;

namespace Business.ModelDTO
{
    public class OrganizationDTO : OrganizationForCreationDTO
    {
        public Guid Id { get; set; }
        public RegionDTO Region { get; set; }
        public Guid LastModifiedby { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
