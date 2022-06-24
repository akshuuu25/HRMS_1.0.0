using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Assignee
    {
        [Key]
        public int AssigneeId { get; set; }

        [Required(ErrorMessage ="Select AssigneeName")]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string AssigneeName { get; set; }     
    }
}
