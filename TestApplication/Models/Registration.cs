using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TestApplication.Models
{
    public class Registration
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public required string FirstName { get; set; }
        [Display(Name = "Midle Name")]
        public string? MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]

        public required string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public required DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [Phone()]
        public required string Phone { get; set; }
        [Required]
        public required string Country { get; set; }
        [Required]
        public required string State { get; set; }
        [Required]
        public required string City { get; set; }
    }
}
