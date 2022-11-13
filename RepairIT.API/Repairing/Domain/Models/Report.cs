namespace RepairIT.API.Repairing.Domain.Models;

public class Report
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int DeviceId { get; set; }
    
    public int TechnicianId { get; set; }

    public Technician Technician { get; set; } = null!;
}