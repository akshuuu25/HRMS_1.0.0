using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class CreateRole
    {
        [Key]
        public int Id { get; set; }  

        [Required]
        public string Username { get; set; }

        [Required]

        public string Rolename { get; set; } 

    }
}
