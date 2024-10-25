namespace Glenavon.JFC.WebApp.Controllers;

public class PartnershipsController : Controller
{
    public IActionResult Index()
    {
        var vm = new PartnershipsViewModel()
        {
        };


        return View(vm);
    }
}