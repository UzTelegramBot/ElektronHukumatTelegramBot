using System.ComponentModel.DataAnnotations;

namespace Business.ModelDTO
{
    public class Login
    {
        [Required]
        [Display(Name ="Login")]
        public string LoginDTO { get; set; }
        [Required]
        [Display(Name ="Password")]
        public string Password { get; set; }
    }
}
