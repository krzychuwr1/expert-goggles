using System;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Skype.Model
{
    public class SkypeTextMessageEntry : ITextMessageEntry
    {
        public string Chatname { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string AuthorDisplayName { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}