using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using static System.Console;

namespace Northwind.Mvc.Controllers;

public class RolesController : Controller
{
    private string AdminRole = "Administrators";
    private string UserEmail = "test1@example.com";
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<IdentityUser> userManager;
    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager) // identify services and pass values to members
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        if (!(await roleManager.RoleExistsAsync(AdminRole))) // if admin role does not exist, create it
        {
            await roleManager.CreateAsync( new IdentityRole(AdminRole) );
        }
        IdentityUser user = await userManager.FindByEmailAsync(UserEmail); // email is hardcoded in this example, it should usually be pulled from context
        if (user == null)  // if no user logged in, it creates one, this would not be normal in prod i think
        {
            user = new();
            user.UserName = UserEmail;
            user.Email = UserEmail;
            IdentityResult result = await userManager.CreateAsync(user, "Pa$$w0rd");
            if (result.Succeeded)
            {
                WriteLine($"User {user.UserName} created successfully.");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    WriteLine(error.Description);
                }
            }
        }
        if (!user.EmailConfirmed)  // needs to confirm email 
        {
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            IdentityResult result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                WriteLine($"User {user.UserName} email confirmed successfully");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    WriteLine(error.Description);
                }
            }
        }
        if (!(await userManager.IsInRoleAsync(user, AdminRole)))  // if user is not in "Administrators" add that user to role
        {
            IdentityResult result = await userManager.AddToRoleAsync(user, AdminRole);
            if (result.Succeeded)
            {
                WriteLine($"User {user.UserName} added to {AdminRole} successfully.");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    WriteLine(error.Description);
                }
            }
        }
        return Redirect("/"); // goes back to home page
    }
}

