using System;

namespace SkypeReader.Model
{
    public class SkypeCallEntry
    {
        public string HostIdentity { get; set; }
        public string Topic { get; set; }
        public DateTime? BeginTimestamp { get; set; }
        public int? ActiveMembers { get; set; }
    }
}