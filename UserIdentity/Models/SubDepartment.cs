using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class SubDepartment

    {
        public int DeptID { get; set; }

        [Key]
        [Required(ErrorMessage = "Enter Sub-Department")]
        public int SDeptID { get; set; }



        [Required(ErrorMessage = "Enter Sub-Department")]
        public string SubDeptName { get; set; }

    }
}
