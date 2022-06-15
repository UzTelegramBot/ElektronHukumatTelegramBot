using System;

namespace Domains
{
    public class Base : BaseEntity
    {
        public Guid Creaetedby { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid LastModifiedby { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
