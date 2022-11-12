using System.ComponentModel.DataAnnotations;

namespace RepairIT.API.Repairing.Resources;

public class SaveReportResource
{
    [Required]
    [MaxLength(150)]
    public string Description { get; set; } = null!;
    
    [Required]
    public int DeviceId { get; set; }
    
    [Required]
    public int TechnicianId { get; set; }
}