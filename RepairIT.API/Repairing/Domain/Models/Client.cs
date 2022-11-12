

namespace RepairIT.API.Repairing.Domain.Models;

public class Client
{
    public int Id { get; set; }
    public string CellPhoneNumber { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string District { get; set; }
    public string Email { get; set; }
    public string DateBirth { get; set; }
    public string Password { get; set; }
    
    
    //Relationships
    
    public List<Device> Devices { get; set; }
}