using System.ComponentModel.DataAnnotations;
using CollegeApp.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CollegeApp.Models
{
    public class StudentDTO
    {
        [Required(ErrorMessage = "Student name is required")] // validation attribute or restiricting the input 
        public int Id { get; set; }

        //[ValidateNever]
        [StringLength(100)]
        public string? StudentName { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid Mail address")]
        public string? Email { get; set; }

        //[Range(10,20)]
        //public int Age { get; set; }

        [Required]
        public string? Address { get; set; }

        //public string? Password { get; set; }
        //// [Compare("Password")] // sometimes this might go wrong so
        //[Compare(nameof(Password))] // this is the right to do it

        //public string? ConfirmPassword { get; set; }

        //[DateCheck]
        //public DateTime AdmissionDate { get; set; }
    }
}
// Note there are a validators for almost everything for more information check the microsoft page for Built-in attribute validators