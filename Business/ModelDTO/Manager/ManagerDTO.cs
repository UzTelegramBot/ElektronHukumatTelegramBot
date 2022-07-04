using System;
using System.ComponentModel.DataAnnotations;

namespace Business.ModelDTO
{
    public class ManagerDTO : ManagerForCreationDTO
    {
        [Required]
        public Guid Id { get; set; }
        public RegionDTO Region { get; set; }
        public OrganizationDTO Organization { get; set; }
    }
}
