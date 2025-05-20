namespace Glenavon.JFC.WebApp.Controllers;

public class TeamsController : Controller
{
    private readonly string _filePath = "wwwroot/data/teams.json";

    public IActionResult Index()
    {
        var vm = new TeamsViewModel
        {
            Teams = LoadTeams()
        };

        return View(vm);
    }

    private List<TeamModel> LoadTeams()
    {
        if (!System.IO.File.Exists(_filePath)) return [];
        var jsonData = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<TeamModel>>(jsonData) ?? [];
    }
}