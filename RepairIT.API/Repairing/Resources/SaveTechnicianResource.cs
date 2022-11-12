using System.ComponentModel.DataAnnotations;

namespace RepairIT.API.Repairing.Resources;

public class SaveTechnicianResource
{
    
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    

    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string DateBirth { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string CellPhoneNumber { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string Password { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string District { get; set; }
}