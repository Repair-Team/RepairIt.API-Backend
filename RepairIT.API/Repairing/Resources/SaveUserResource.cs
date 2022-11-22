using System.ComponentModel.DataAnnotations;

namespace RepairIT.API.Repairing.Resources;

public class SaveUserResource
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; }= null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; }= null!;

    [Required] public bool IsTechnician { get; set; } = false;
    [Required] public bool IsPremium { get; set; } = false;
}