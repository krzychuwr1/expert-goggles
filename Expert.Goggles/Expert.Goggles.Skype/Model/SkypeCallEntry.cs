using System;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Skype.Model
{
    public class SkypeCallEntry : ICallEntry
    {
        public string HostIdentity { get; set; }
        public string Topic { get; set; }
        public DateTime? BeginTimestamp { get; set; }
        public int? ActiveMembers { get; set; }
    }
}