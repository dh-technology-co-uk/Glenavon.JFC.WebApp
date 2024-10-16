namespace Glenavon.JFC.WebApp.Controllers;

public class AccountController : Controller
{
    private const string DefaultEmail = "testuser@example.com";
    private const string DefaultPassword = "Password123";

    // GET: /Account/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password, bool rememberMe)
    {
        // Check if the email and password match the default values
        if (email == DefaultEmail && password == DefaultPassword)
        {
            // Create claims for the user
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, email),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, "Admin") // Adding a default role
            };

            // Create the claims identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Set expiration for the cookie
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe, // Keep the cookie persistent if 'Remember Me' is checked
                ExpiresUtc = DateTime.UtcNow.AddDays(5) // Set cookie expiration to X days (e.g., 5 days)
            };

            // Sign in the user with the identity and properties
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            // Redirect to the home page after login
            return RedirectToAction("Index", "Home");
        }

        // If the login attempt fails, display an error message
        ModelState.AddModelError("", "Invalid login attempt.");
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        // Sign out the user
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    // GET: /Account/ForgotPassword
    public IActionResult ForgotPassword()
    {
        return View();
    }

    // POST: /Account/ForgotPassword
    [HttpPost]
    public IActionResult ForgotPassword(string email)
    {
        // Handle sending the password reset instructions here
        // For now, just redirect to the login page after submission
        TempData["Message"] = "Password reset instructions have been sent to your email.";
        return RedirectToAction("Login");
    }


    // GET: /Account/Register
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string email, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            ModelState.AddModelError("", "Passwords do not match.");
            return View();
        }

        // Handle account creation logic here.

        return RedirectToAction("Index", "Home");
    }
}