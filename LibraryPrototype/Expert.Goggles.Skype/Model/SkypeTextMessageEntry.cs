﻿using System;

namespace Expert.Goggles.Skype.Model
{
    public class SkypeTextMessageEntry
    {
        public string Chatname { get; set; }
        public string ContentXml { get; set; }
        public string AuthorSkypeName { get; set; }
        public string AuthorDisplayName { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}