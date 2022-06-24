
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models
{
    public class OnBoardingCheckPoint
    {
        [Key]
        public int CheckPointId { get; set; }

        [Required(ErrorMessage = "Please enter checkpoint")]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string CheckPointName { get; set; }

        [Required(ErrorMessage = "Please select businessunit")]
        public int BUnitId { get; set; }

        [Required(ErrorMessage = "Please select department")]
        public int DeptID { get; set; }

        [Required(ErrorMessage = "Please select assignee")]
        public int AssigneeId { get; set; }

        [StringLength(100, ErrorMessage = "The name cannot be more than 10 characters")]
        public string Description { get; set; }

        // public OnBoardingCheckPointViewModel checkpoint { get; set; }



        [NotMapped]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string BUnitName { get; set; }

        [NotMapped]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string DeptName { get; set; }

        [NotMapped]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]

        public string AssigneeName { get; set; }


    }
}
