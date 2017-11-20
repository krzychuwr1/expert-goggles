using System;

namespace Expert.Goggles.Skype.Model
{
    public class SkypeContactEntry
    {
        public string SkypeName { get; set; }
        public string PstnNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}