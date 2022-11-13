namespace RepairIT.API.Repairing.Domain.Models;

public class Device
{
    public int Id { get; set; }
    public string name { get; set; } = null!;
    public string description { get; set; } = null!;
    public string imagePath { get; set; } = null!;
    public string inventoryStatus { get; set; } = null!;

    public int ClientId { get; set; }
    
    public Client Client { get; set; } = null!;
}