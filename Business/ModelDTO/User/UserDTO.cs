using System.ComponentModel.DataAnnotations;

namespace Business.ModelDTO
{
    public class UserDTO : UserForCreationDTO
    {
        [Required]
        public long Id { get; set; }
    }
}
