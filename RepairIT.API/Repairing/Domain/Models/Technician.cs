namespace RepairIT.API.Repairing.Domain.Models;

public class Technician
{
    public int Id { get; set; }
    public string CellPhoneNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string District { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string DateBirth { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    //Relationships
    
    public List<Report> Reports { get; set; } = null!;
}