namespace Glenavon.JFC.WebApp.Controllers;

public class CommitteeController : Controller
{
    public IActionResult Index()
    {
        var vm = new CommitteeViewModel
        {
            CommitteeMembers = new List<CommitteeModel>
            {
                new()
                {
                    Name = "Bill Price",
                    Roles = new List<string> { "Chairman", "Pitch Maintenance" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Paul Tyler",
                    Roles = new List<string> { "Club Secretary", "Boys Secretary" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Chris Banks",
                    Roles = new List<string>
                        { "Assistant Club Secretary", "Girls Secretary", "Registration Secretary (Love Admin)" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Ben Mighall",
                    Roles = new List<string> { "Vice Chairman", "Treasurer", "Finance (Grants)", "Kits Enquiry" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Mike Wallace",
                    Roles = new List<string> { "Welfare Officer", "Comms", "Finance (Grants)" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Joe Duckworth",
                    Roles = new List<string> { "Welfare Officer", "Comms", "Equipment" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Tony Williams",
                    Roles = new List<string> { "Welfare Officer", "Lead Safeguarding" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Ruth Duncan",
                    Roles = new List<string> { "Welfare Officer" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Kane Breadner",
                    Roles = new List<string> { "Tournaments" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Carl Hughes",
                    Roles = new List<string> { "Pitch Maintenance" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Steve Mylett",
                    Roles = new List<string> { "Comms" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Martin Shaunessey",
                    Roles = new List<string> { "Fixture Secretary" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Carl Sulivan",
                    Roles = new List<string> { "Member" },
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Daniel Hulmston",
                    Roles = new List<string> { "Website" }, 
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Benjamin Mash",
                    Roles = new List<string> { "Sponsorship" }, 
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "John Killen",
                    Roles = new List<string> { "President" }, 
                    ImagePath = "/images/committee/committee_logo.png;"
                },
                new()
                {
                    Name = "Anthony Sheehan",
                    Roles = new List<string> { "Development Boys & Girls", "Presentation" },
                    ImagePath = "/images/committee/committee_logo.png;"
                }
            }
        };

        return View(vm);
    }
}