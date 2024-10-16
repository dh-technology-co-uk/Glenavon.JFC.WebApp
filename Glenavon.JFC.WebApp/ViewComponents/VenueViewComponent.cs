namespace Glenavon.JFC.WebApp.ViewComponents;

public class VenueViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(VenueModel model)
    {
        return View("Index", model);
    }
}