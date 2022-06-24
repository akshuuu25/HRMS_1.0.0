using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Country
    {
        [Key]


        public int CountryId { get; set; }
        public string SortName { get; set; }

        [StringLength(20, ErrorMessage = "The name cannot be more than 10 characters")]
        public string CountryName { get; set; }
        

    }
}
