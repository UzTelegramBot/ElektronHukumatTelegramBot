using System;
using System.Collections.Generic;

namespace Domains
{
    public class Manager : ABase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public RoleManager Role { get; set; }
        public Guid? OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
