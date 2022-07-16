using System;
using System.ComponentModel.DataAnnotations;

namespace Business.ModelDTO
{
    public class MessageForCreationDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ManagerId { get; set; }
        [Required]
        public Guid RegionId { get; set; }

    }
}
