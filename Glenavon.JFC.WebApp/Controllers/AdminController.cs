namespace Glenavon.JFC.WebApp.Controllers;

[Authorize(Roles = "Admin")] // Ensure only authenticated users with the 'Admin' role can access this page
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}