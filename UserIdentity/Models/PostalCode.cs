using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class PostalCode
    {
        public int CityId { get; set; }

        [Key]
        public int PostalId { get; set; }
      
        [Required]
        public string PostalNo { get; set; }
    }
}
