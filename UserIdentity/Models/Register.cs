using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Please Enter Your FullName")]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string FullName { get; set; }


        [Required(ErrorMessage = "Please Select Your Gender")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Please Select Your Country")]
        public int CountryId { get; set; }





        [Required(ErrorMessage = "Please Select Your State")]
        public int StateId { get; set; }



        [Required(ErrorMessage = "Please Select Your City")]
        public int CityId { get; set; }




        [Required(ErrorMessage = "Please Select Your Postal-Code")]
        public int PostalId { get; set; }


        [Required(ErrorMessage = "Please Enter Your Address")]
        [StringLength(50, ErrorMessage = "The name cannot be more than 10 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email")]
        [EmailAddress]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter,and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Your Confirm-Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter,and one special character.")]
        public string ConfirmPassword { get; set; }


    }
}
