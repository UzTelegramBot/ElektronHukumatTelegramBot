using Domains;
using System;

namespace Business.ModelDTO
{
    public class MessageDTO : MessageForCreationDTO
    {
        public Guid guid { get; set; }
        public Manager Manager { get; set; }
        public Region Region { get; set; }
    }
}
