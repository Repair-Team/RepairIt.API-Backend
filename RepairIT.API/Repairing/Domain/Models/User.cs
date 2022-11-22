namespace RepairIT.API.Repairing.Domain.Models;

public class User
{
    public int Id;
    public string FirstName = null!;
    public string LastName = null!;
    public string Email = null!;
    public string Password = null!;
    public bool IsTechnician = false;
    public bool IsPremium = false;
    
    public List<Device> Devices { get; set; }
}