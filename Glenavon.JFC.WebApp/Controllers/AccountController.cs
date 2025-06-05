namespace Glenavon.JFC.WebApp.Controllers;

public class AccountController : Controller
{
    private const string AdminEmail = "admin@glenavonjfc.co.uk";
    private const string ManagerEmail = "manager@glenavonjfc.co.uk";
    private const string SuperAdmin = "superadmin@glenavonjfc.co.uk";
    private const string AdminPassword = "Glenavon2025!";
    private const string ManagerPassword = "Manager2025!";
    private const string SuperAdminPassword = "!!!!!Glenavon2025!!!!!";

    // GET: /Account/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password, bool rememberMe)
    {
        var userRole = email switch
        {
            // Determine the user's role based on email and password
            AdminEmail when password == AdminPassword => "Admin",
            ManagerEmail when password == ManagerPassword => "Manager",
            SuperAdmin when password == SuperAdminPassword => "SuperAdmin",
            _ => null
        };

        if (userRole != null)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, email),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, userRole) // Assign role dynamically
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = DateTime.UtcNow.AddDays(5)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Dashboard");
        }

        ModelState.AddModelError("", "Invalid login attempt.");
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [Route("/Account/AccessDenied")]
    [AllowAnonymous]
    public IActionResult AccessDenied(string returnUrl = "/")
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [Route("/NotFound")]
    [AllowAnonymous]
    public override NotFoundResult NotFound()
    {
        return base.NotFound();
    }

}