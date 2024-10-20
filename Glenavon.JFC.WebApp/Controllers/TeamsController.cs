namespace Glenavon.JFC.WebApp.Controllers;

public class TeamsController : Controller
{
    public IActionResult Index()
    {
        var vm = new TeamsViewModel()
        {
            Teams = new List<TeamModel>
            {
                new()
                {
                    Name = "U7 Eagles",
                    Manager = "Ben Mash",
                    Coaches = new List<string>
                        { "Dan Hulmston" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "U7 Harriers",
                    Manager = "Ben Mash",
                    Coaches = new List<string>
                        { "Dan Hulmston" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "U7 Hawks",
                    Manager = "Ben Mash",
                    Coaches = new List<string>
                        { "Dan Hulmston" },
                    ImagePath = "/images/committee/committee_logo.png;"
                }
            }
        };

        return View(vm);
    }
}