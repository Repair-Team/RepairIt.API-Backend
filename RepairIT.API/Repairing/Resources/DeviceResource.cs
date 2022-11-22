namespace RepairIT.API.Repairing.Resources;

public class DeviceResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public string InventoryStatus { get; set; }
    
    public int UserId { get; set; }
}