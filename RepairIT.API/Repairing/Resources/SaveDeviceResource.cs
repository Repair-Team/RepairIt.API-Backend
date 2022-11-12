using System.ComponentModel.DataAnnotations;

namespace RepairIT.API.Repairing.Resources;

public class SaveDeviceResource
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(90)]
    public string Description { get; set; } = null!;

    [Required]
    public string ImagePath { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string InventoryStatus { get; set; } = null!;

    [Required]
    public int ClientId { get; set; }
}