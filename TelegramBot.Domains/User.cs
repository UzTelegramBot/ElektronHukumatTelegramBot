using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBot.Domains
{
    public class User
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Language Language { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
    }
}
