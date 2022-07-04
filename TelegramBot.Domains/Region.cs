using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBot.Domains
{
    public class Region
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public long RegionIndex { get; set; }
        public string UzName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
