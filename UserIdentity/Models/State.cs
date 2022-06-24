using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class State
    {
        public int CountryId { get; set; }
        [Key]
        public int StateId { get; set; }
       

        [Required]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string StateName { get; set; }


    }
}
