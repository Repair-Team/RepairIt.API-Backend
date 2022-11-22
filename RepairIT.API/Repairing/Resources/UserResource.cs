namespace RepairIT.API.Repairing.Resources;

public class UserResource
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; }= null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; }= null!;
    public bool IsTechnician { get; set; }
    public bool IsPremium { get; set; }
}