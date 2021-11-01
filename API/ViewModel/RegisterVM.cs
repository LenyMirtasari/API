using API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class RegisterVM

    {

       /* public Employee employeeVm { get; set; }
        public Account accountVm { get; set; }
        public Education educationVm { get; set; }*/

        [Required(ErrorMessage = "Please enter your NIK")]
      //  [RegularExpression(@"^.{11,}([0-9]+)$", ErrorMessage = "Minimum 12 characters required and Please enter valid Number")]
        public string NIK { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
       // [RegularExpression(@"^.{10,}([0-9]+)$", ErrorMessage = "Minimum 12 characters required and Please enter valid Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your birth date")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Please enter your salary")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your Gender")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter your degree")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "Please enter your GPA")]
        public string Gpa { get; set; }
        [Required(ErrorMessage = "Please enter your University")]
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }

        /*  [Range(10, 20)]
          public int Age { get; set; }
  */
    }
}
