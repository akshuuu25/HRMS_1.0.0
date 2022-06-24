using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class BusinessUnit

    {  
        
      [Key]
        
        public int BUnitId { get; set; }   

        [Required(ErrorMessage = "Enter Business Unit")]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
       
        public string BUnitName { get; set; }

        [Required(ErrorMessage = "Select Status")]
        public bool Status { get; set; }

    }
}
