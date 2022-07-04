using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBot.Domains
{
    public class BotTextData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Uz { get; set; }
        public string Ru { get; set; }
        public string Eng { get; set; }
        public TypeData TypeData { get; set; }
    }
}
