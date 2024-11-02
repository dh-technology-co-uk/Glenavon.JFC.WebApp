namespace Glenavon.JFC.WebApp.Controllers;

public class TeamsController : Controller
{
    public IActionResult Index()
    {
        var vm = new TeamsViewModel
        {
            Teams = new List<TeamModel>
            {
                new()
                {
                    Name = "Belles Ladies", Manager = "Ian Wallace", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "Glenavon Athletic", Manager = "Phil Ainscough", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U07 Belles Stars", Manager = "Hanna Hollins", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U07 Eagles", Manager = "Ado Mercer",
                    Coaches = new List<string> { "Joe Duckworth", "Mike Foulkes" },
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U07 Harriers", Manager = "Phil Williams", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U07 Hawks", Manager = "Ben Mash", Coaches = new List<string> { "Dan Hulmston" },
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U08 Belles Diamonds", Manager = "Lee Doyle", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U08 Belles Girls", Manager = "Matthew Ward", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U08 Eagles", Manager = "Scott Lamb", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U08 Falcons", Manager = "Neal Pollock", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U08 Hawks", Manager = "Matt Bode", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U09 Belles Diamonds", Manager = "Graeme Harris", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U09 Kestrels", Manager = "Sarah Parsonage", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U09 Belles Stars", Manager = "Chris Hawthorn", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U09 Eagles", Manager = "Sean May", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U09 Falcons", Manager = "Joe Duckworth", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U09 Hawks", Manager = "Adam Cavanagh", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U09 Kestrels", Manager = "Mark Bainbridge", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U10 Belles Diamonds", Manager = "Ian Moore", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U10 Eagles", Manager = "Dave Evans", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U10 Falcons", Manager = "Sean Keane", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U10 Hawks", Manager = "Brian Hewitt", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U11 Belles Stars", Manager = "Danny Williams", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U11 Belles Diamonds", Manager = "Millie Briscoe", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U11 Eagles", Manager = "Paul Phennah", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U11 Falcons", Manager = "Trevor Billo", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U11 Hawks", Manager = "Mike Doherty", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U11 Kestrels", Manager = "Mike Sadler", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U12 Belles Diamonds", Manager = "Chris Cunningham", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U12 Eagles", Manager = "Ray Cox", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U12 Falcons", Manager = "Craig Cartwright", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U12 Hawks", Manager = "Danny Brierley", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U13 Belles Diamonds", Manager = "Chrissy Banks", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U13 Belles Girls", Manager = "Ian Wallace", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U13 Belles Stars", Manager = "Steve Jones", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U13 Eagles", Manager = "Adam Morgan", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U13 Falcons", Manager = "Tim Brandreth", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U14 Falcons", Manager = "Matt Webb", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U14 Harriers", Manager = "Andy Robertson", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U14 Hawks", Manager = "Gaz Richards", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U15 Belles Diamonds", Manager = "Phil Bryan", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U15 Eagles", Manager = "Dave Evans", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U15 Falcons", Manager = "Martin Shaughnessy", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U15 Kestrels", Manager = "Paul Freaney", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U16 Belles Girls", Manager = "Baz Wallace", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U16 Falcons", Manager = "Ken Rademaker", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U16 Hawks", Manager = "Ant Sheehan", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U16 Kestrels", Manager = "Matt Hogg", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U18 Eagles", Manager = "Rob Seabury", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                },
                new()
                {
                    Name = "U18 Belles", Manager = "Darren Gibson", 
                    ImagePath = "/images/committee/committee_logo.png", League = "Eastham Junior Football League"
                }
            }
        };

        return View(vm);
    }
}