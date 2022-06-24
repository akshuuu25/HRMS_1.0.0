using System.ComponentModel.DataAnnotations;

namespace HRMS.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
