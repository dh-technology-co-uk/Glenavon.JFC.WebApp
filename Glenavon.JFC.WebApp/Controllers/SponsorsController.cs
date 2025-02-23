namespace Glenavon.JFC.WebApp.Controllers;

public class SponsorsController : Controller
{
    private readonly string _filePath = "wwwroot/data/sponsors.json";

    public IActionResult Index()
    {
        var vm = new SponsorsViewModel
        {
            Sponsors = LoadSponsors()
        };

        return View(vm);
    }

    private List<SponsorModel> LoadSponsors()
    {
        if (!System.IO.File.Exists(_filePath)) return new List<SponsorModel>();
        var jsonData = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<SponsorModel>>(jsonData) ?? new List<SponsorModel>();
    }
}