using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSAT.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        [NotMapped]
        public string RoleId { get; set; } = string.Empty;

        [NotMapped]
        public string Role { get; set; } = string.Empty;

        [NotMapped]
        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}
