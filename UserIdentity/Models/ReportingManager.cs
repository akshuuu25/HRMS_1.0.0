using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class ReportingManager
    { 

     [Key]
    [Required(ErrorMessage = "Enter Reporting Manager")]
    public int ReportID { get; set; }

    [Required(ErrorMessage = "Enter Reporting Manager")]
    public string ReportManagerName { get; set; }

}
}
