using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Department
    {
        [Key]
        [Required(ErrorMessage = "Enter Department")]
        public int DeptID { get; set; }

        [Required(ErrorMessage = "Enter Department")]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string DeptName { get; set; }

        [Required(ErrorMessage = "Enter Status")]
        public bool Status { get; set; }
    }
}
