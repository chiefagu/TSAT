using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TSAT.Data;
using TSAT.Models;

namespace TSAT.Controllers;

[Authorize(Roles = "Admin")]
public class RolesController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var roles = _db.Roles.ToList();

        return View(roles);
    }

    [HttpGet]
    public IActionResult Upsert(string id)
    {
        if (String.IsNullOrEmpty(id))
        {
            return View();
        }
        else
        {
            //update
            var role = _db.Roles.FirstOrDefault(r => r.Id == id);

            return View(role);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Upsert(IdentityRole role)
    {
        if (await _roleManager.RoleExistsAsync(role.Name))
        {
            TempData["Error"] = "Role already exists";
            return RedirectToAction(nameof(Index));
        }

        if (string.IsNullOrEmpty(role.Id))
        {
            // create
            await _roleManager.CreateAsync(new IdentityRole() { Name = role.Name });
            TempData["Success"] = "Role created successfully";
        } else
        {
            //update
            var roleFrmDb = _db.Roles.FirstOrDefault(r => r.Id == role.Id);
            if (roleFrmDb == null)
            {
                TempData["Error"] = "Role not found";
                return RedirectToAction(nameof(Index));
            };

            roleFrmDb.Name = role.Name;
            roleFrmDb.NormalizedName = role.Name.ToUpper();

            var result = await _roleManager.UpdateAsync(roleFrmDb);
            TempData["Success"] = "Role updated successfully";

        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var roleFrmDb = _db.Roles.FirstOrDefault(r => r.Id == id);
        if (roleFrmDb == null)
        {
            TempData["Error"] = "No such role exists";
            return RedirectToAction(nameof(Index));
        }

        var userRolesFrmDb = _db.UserRoles.Where(ur => ur.RoleId == id).Count();

        if (userRolesFrmDb > 0)
        {
            TempData["Error"] = "Cannot delete this role, users are assigned to this role";
            return RedirectToAction(nameof(Index));
        }

        await _roleManager.DeleteAsync(roleFrmDb);
        TempData["Success"] = "Role deleted successfully";
        return RedirectToAction(nameof(Index));

    }
}

