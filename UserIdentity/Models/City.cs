using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class City
    {
        public int StateId { get; set; }
        [Key]
        public int CityId { get; set; }
       
        
        [Required]
        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string CityName { get; set; }
    }
}
