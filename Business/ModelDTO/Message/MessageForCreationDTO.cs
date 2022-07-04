using System;

namespace Business.ModelDTO
{
    public class MessageForCreationDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ManagerId { get; set; }
        public Guid RegionId { get; set; }

    }
}
