namespace Glenavon.JFC.WebApp.Controllers;

public class CommitteeController : Controller
{
    private readonly string _filePath = "wwwroot/data/committee.json";

    public IActionResult Index()
    {
        var vm = new CommitteeViewModel
        {
            CommitteeMembers = LoadCommittee()
        };

        return View(vm);
    }

    private List<CommitteeModel> LoadCommittee()
    {
        if (!System.IO.File.Exists(_filePath)) return new List<CommitteeModel>();
        var jsonData = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<CommitteeModel>>(jsonData) ?? new List<CommitteeModel>();
    }
}