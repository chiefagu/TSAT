using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TSAT.Data;
using TSAT.Models;

namespace TSAT.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _rolemanager;

    public UserController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> rolemanager)
    {
        _db = db;
        _userManager = userManager;
        _rolemanager = rolemanager;
    }


    public IActionResult Index()
    {
        // retrieve all the users frm the db
        var users = _db.Users.ToList();

        var userRole = _db.UserRoles.ToList();

        var roles = _db.Roles.ToList();

        foreach (var user in users)
        {
            var role = userRole.FirstOrDefault(ur => ur.UserId == user.Id);

            if (role == null)
            {
                user.Role = "None";
            }

            else
            {
                user.Role = roles.FirstOrDefault(r => r.Id == role.RoleId)!.Name;
            }

            //return View(users);

        }

        return View(users);
    }


    public IActionResult Edit(string userId)
    {
        // retrieve all the users frm the db
        var user = _db.Users.FirstOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return NotFound();
        }

        var userRole = _db.UserRoles.ToList();
        var roles = _db.Roles.ToList();
        var role = userRole.FirstOrDefault(ur => ur.UserId == user.Id);

        if (role != null)
        {
            user.RoleId = roles.FirstOrDefault(r => r.Id == role.RoleId)!.Id;
        }

        user.RoleList = _db.Roles.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = r.Name, Value = r.Id });

        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ApplicationUser data)
    {
        if (ModelState.IsValid)
        {

            var user = await _userManager.FindByEmailAsync(data.Email);

            if (user == null) return NotFound();

            var userRole = _db.UserRoles.FirstOrDefault(u => u.UserId == user.Id);
            //var role = _rolemanager.a
            
            if (userRole != null)
            {
                //remove previously assigned role
                var previousRoleName = _db.Roles.Where(r => r.Id == userRole.RoleId).Select(r => r.Name).FirstOrDefault();

                await _userManager.RemoveFromRoleAsync(user, previousRoleName);

            }

            // add the new role
            await _userManager.AddToRoleAsync(
                user,
                _db.Roles.FirstOrDefault(r => r.Id == data.RoleId)!.Name
            );

            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            _db.SaveChanges();
            TempData["Success"] = "User has been edited successfully";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            data.RoleList = _db.Roles.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = r.Name, Value = r.Id });

            return View(data);
        }

    }

    [HttpPost]
    public async Task<IActionResult> Delete(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        _db.Users.Remove(user);
        _db.SaveChanges();
        TempData["Success"] = "User deleted successfully";
        return RedirectToAction(nameof(Index));
    }

   
    public async Task<IActionResult> ManageUserClaims(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var model = new UserClaimsViewModel() { UserId = userId };

        foreach(Claim claim in ClaimStore.ClaimList)
        {
            UserClaim userClaim = new UserClaim() { ClaimType = claim.Type };

            model.Claims.Add(userClaim);
        }

        return View(model);
        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel data)
    {
        var user = await _userManager.FindByIdAsync(data.UserId);

        if (user == null)
        {
            return NotFound();
        }

        var claims = data.Claims
            .Where(c => c.IsSelected)
            .Select(c => new Claim(c.ClaimType, c.IsSelected.ToString()));


        var result = await _userManager.AddClaimsAsync(user, claims);

        TempData["Success"] = "Claims updated successfully";
        return RedirectToAction(nameof(Index));
    }
}

