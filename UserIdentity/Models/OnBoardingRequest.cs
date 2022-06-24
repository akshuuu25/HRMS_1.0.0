using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models
{
    public class OnBoardingRequest

    { 
        [Key]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Enter Firstname!!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed!!")]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Enter Lastname")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed!!")]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Enter Email Address")]
        [DataType(DataType.EmailAddress)]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Mobile No.")]
        
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Enter Date of Joining")]
        public DateTime Doj { get; set; }

        [Required(ErrorMessage = "Enter Designation")]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Please Select Business Unit ")]
        public int BUnitId { get; set; }


        [Required(ErrorMessage = "Please Select Department")]
        public int DeptID { get; set; }

        [Required(ErrorMessage = "Please Select Sub-Department")]
        public int SDeptID { get; set; }

        [Required(ErrorMessage = "Select Reporting Manager")]
        public int ReportID { get; set; }

        [NotMapped]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string BUnitName { get; set; }

        [NotMapped]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string DeptName { get; set; }


        [NotMapped]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string ReportManagerName { get; set; }


        [NotMapped]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string SubDeptName { get; set; }



    }
}
