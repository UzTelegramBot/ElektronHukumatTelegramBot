using System;
using System.ComponentModel.DataAnnotations;

namespace Business.ModelDTO
{
    public class OrganizationForCreationDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public string MessageTitle { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public Guid Creaetedby { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ParentId { get; set; }
    }
}
