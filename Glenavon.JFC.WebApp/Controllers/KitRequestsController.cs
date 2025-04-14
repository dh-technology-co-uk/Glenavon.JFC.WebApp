using System.Text.RegularExpressions;

namespace Glenavon.JFC.WebApp.Controllers;

[Authorize(Roles = "Manager,Admin")]
public class KitRequestsController : Controller
{
    private readonly string _filePath = "wwwroot/data/teams.json";
    private readonly string _directoryPath = "wwwroot/data/kitrequests";

    public IActionResult Index()
    {
        var vm = new KitsRequestsViewModel
        {
            Teams = LoadTeams()
        };

        return View(vm);
    }

    private List<TeamModel> LoadTeams()
    {
        if (!System.IO.File.Exists(_filePath)) return new List<TeamModel>();
        var jsonData = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<TeamModel>>(jsonData) ?? new List<TeamModel>();
    }

    [HttpPost]
    [Route("KitRequests/SubmitTeam")]
    public IActionResult SubmitTeam([FromBody] KitRequestModel request)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            int nextRequestNumber = GetNextAvailableRequestNumber();

            request.Id = nextRequestNumber; // 👈 Assign the ID here

            string fileName = $"kitrequest-{nextRequestNumber}.json";
            string filePath = Path.Combine(_directoryPath, fileName);

            string json = JsonConvert.SerializeObject(request, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return Ok(new
            {
                success = true,
                requestNumber = nextRequestNumber,
                message = "Team request submitted successfully."
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error saving request: {ex.Message}");
        }
    }


    private int GetNextAvailableRequestNumber()
    {
        var existingFiles = Directory.GetFiles(_directoryPath, "kitrequest-*.json");
        int highest = 0;

        var regex = new Regex(@"kitrequest-(\d+)\.json");

        foreach (var file in existingFiles)
        {
            var match = regex.Match(Path.GetFileName(file));
            if (match.Success && int.TryParse(match.Groups[1].Value, out int num))
            {
                highest = Math.Max(highest, num);
            }
        }

        return highest + 1;
    }
}