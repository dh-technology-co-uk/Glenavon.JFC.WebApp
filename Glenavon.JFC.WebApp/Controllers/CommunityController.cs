namespace Glenavon.JFC.WebApp.Controllers;

public class CommunityController : Controller
{
    public IActionResult Index()
    {
        var vm = new CommunityViewModel();

        return View(vm);
    }
}