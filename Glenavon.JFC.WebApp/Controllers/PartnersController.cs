namespace Glenavon.JFC.WebApp.Controllers;

public class PartnersController : Controller
{
    private readonly string _filePath = "wwwroot/data/partners.json";

    public IActionResult Index()
    {
        var vm = new PartnersViewModel
        {
            Partners = LoadPartners()
        };

        return View(vm);
    }

    private List<PartnerModel> LoadPartners()
    {
        if (!System.IO.File.Exists(_filePath)) return new List<PartnerModel>();
        var jsonData = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<PartnerModel>>(jsonData) ?? new List<PartnerModel>();
    }
}