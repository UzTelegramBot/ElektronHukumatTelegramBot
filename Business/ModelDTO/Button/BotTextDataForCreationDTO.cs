using Domains;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.ModelDTO
{
    public class BotTextDataForCreationDTO
    {
        [Required]
        public string Uz { get; set; }
        [Required]
        public string Ru { get; set; }
        [Required]
        public string Eng { get; set; }
        [Required]
        public TypeData TypeData { get; set; }
    }
}
