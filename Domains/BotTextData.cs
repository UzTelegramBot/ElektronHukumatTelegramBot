using System;

namespace Domains
{
    public class BotTextData : BaseEntity
    {
        public string Uz { get; set; }
        public string Ru { get; set; }
        public string Eng { get; set; }
        public TypeData TypeData { get; set; }
    }
}
