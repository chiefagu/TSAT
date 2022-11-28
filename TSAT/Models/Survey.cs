using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSAT.Models;

public class Survey
{
    public int Id { get; set; }

    public string? Name { get; set; }


    public string UserName { get; set; } = string.Empty;

    [Required]
    public int Score { get; set; }

    public string? Comment { get; set; }

    [Required]
    public int Route { get; set; }

    [NotMapped]
    public IEnumerable<SelectListItem>? RatingList { get; set; }

    [NotMapped]
    public IEnumerable<SelectListItem>? RouteList { get; set; }

}

