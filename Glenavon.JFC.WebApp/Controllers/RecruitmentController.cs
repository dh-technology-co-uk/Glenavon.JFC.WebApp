namespace Glenavon.JFC.WebApp.Controllers;
public class RecruitmentController : Controller
{
    private readonly string _filePath = "wwwroot/data/teams.json";

    private List<TeamModel> LoadTeams()
    {
        if (!System.IO.File.Exists(_filePath)) return [];
        var jsonData = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<TeamModel>>(jsonData) ?? [];
    }

    [HttpGet]
    public IActionResult Index()
    {
        var teams = LoadTeams();

        var model = new RecruitmentViewModel
        {
            RecruitingTeams = teams
                .Where(t => t.IsRecruiting)
                .ToList()
        };

        return View(model);
    }
}
