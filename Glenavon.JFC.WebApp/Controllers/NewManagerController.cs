namespace Glenavon.JFC.WebApp.Controllers;

[Authorize(Roles = "Manager,Admin")]
public class NewManagerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}