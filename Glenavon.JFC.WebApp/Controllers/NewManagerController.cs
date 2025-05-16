namespace Glenavon.JFC.WebApp.Controllers;

[Authorize(Roles = "Manager,Admin,SuperAdmin")]
public class NewManagerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}