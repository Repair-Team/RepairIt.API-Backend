namespace RepairIT.API.Repairing.Resources;

public class ReportResource
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;

    public int DeviceId { get; set; }
    
    public int TechnicianId { get; set; }
    
}