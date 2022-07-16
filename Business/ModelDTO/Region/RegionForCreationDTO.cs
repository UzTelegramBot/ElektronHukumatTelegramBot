using System.ComponentModel.DataAnnotations;

namespace Business.ModelDTO
{
    public class RegionForCreationDTO
    {
        [Required]
        public long RegionIndex { get; set; }
        [Required]
        public string UzName { get; set; }
    }
}
