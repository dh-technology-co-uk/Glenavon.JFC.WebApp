namespace Glenavon.JFC.WebApp.Controllers;

public class CommitteeController : Controller
{
    public IActionResult Index()
    {
        var vm = new CommitteeViewModel();

        return View(vm);
    }
}