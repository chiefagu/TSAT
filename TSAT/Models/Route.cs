using System.ComponentModel.DataAnnotations;

namespace TSAT.Models;

public class Route
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int DestinationId { get; set; }

}

