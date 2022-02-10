using System;

namespace CustomerApi
{
    public class Customer
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public Address Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string SocialSecurityId { get; set; }

        public string Occupation { get; set; }

        public int Age => DateOfBirth != null && DateOfBirth > DateTime.MinValue ? DateTime.UtcNow.Subtract(DateOfBirth).Days / 365 : 0;
    }
}
