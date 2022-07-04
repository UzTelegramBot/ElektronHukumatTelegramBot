using System;

namespace Domains
{
    public class Message : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ManagerId { get; set; }
        public Manager Manager { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
    }
}
