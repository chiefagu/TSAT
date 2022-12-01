using System.Security.Claims;

namespace TSAT.Data;

public static class ClaimStore
{
    public static List<Claim> ClaimList = new List<Claim>()
    {
        new Claim("Create", "Create"),
        new Claim("Edit", "Edit"),
        new Claim("Delete", "Delete"),
    };
}
