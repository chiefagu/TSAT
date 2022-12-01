using System.Security.Claims;

namespace TSAT.Models;

public class UserClaimsViewModel
{
    public string UserId { get; set; } = string.Empty;

    public List<UserClaim> Claims { get; set; } = new List<UserClaim>();
}

public class UserClaim
{
    public string ClaimType {  get; set; }  = string.Empty;
    public bool IsSelected { get; set; }
}


