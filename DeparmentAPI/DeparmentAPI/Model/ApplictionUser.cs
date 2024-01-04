using Microsoft.AspNetCore.Identity;
using System;
using System.IO;

namespace DeparmentAPI.Model
{
    public class ApplictionUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public int AadharNumber { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PinCode { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string Religion { get; set; }
        public DateTime DOB { get; set; }
        public string Course { get; set; }
        public string Specialisations { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
