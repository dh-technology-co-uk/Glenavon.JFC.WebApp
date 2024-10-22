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
                    Manager = "Adrian Mercer",
                    Coaches = new List<string>
                        { "Joe Duckworth", "Mike Foulkes" },
                    ImagePath = "/images/committee/committee_logo.png;",
                    League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U7 Harriers",
                    Manager = "Phil Williams",
                    ImagePath = "/images/committee/committee_logo.png;",
                    League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U7 Hawks",
                    Manager = "Ben Mash",
                    Coaches = new List<string>
                        { "Dan Hulmston" },
                    ImagePath = "/images/committee/committee_logo.png;",
                    League = "Eastham Junior Football League"
                }
            }
        };

        return View(vm);
    }
}