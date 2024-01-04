using System;
using System.ComponentModel.DataAnnotations;

namespace DeparmentAPI.Model
{
    public class SignInModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string MotherName { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public int AadharNumber { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int PinCode { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Religion { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string Course { get; set; }
        [Required]
        public string Specialisations { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}