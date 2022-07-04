using Domains;
using System;

namespace Business.ModelDTO
{
    public class UserForCreationDTO
    {
        public Language Language { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RegionId { get; set; }
    }
}
