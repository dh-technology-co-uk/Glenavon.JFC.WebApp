namespace Glenavon.JFC.WebApp.Controllers;

[Authorize(Roles = "Manager,Admin")]
public class EquipmentRequestsController : Controller
{
    private readonly string _directoryPath = "wwwroot/data/equipmentrequests";
    private readonly string _filePath = "wwwroot/data/teams.json";

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

    [HttpPost("EquipmentRequests/SubmitRequest")]
    public async Task<IActionResult> SubmitRequest()
    {
        try
        {
            if (!Directory.Exists(_directoryPath)) Directory.CreateDirectory(_directoryPath);

            var form = await Request.ReadFormAsync();

            // Extract fields
            var teamName = form["TeamName"].FirstOrDefault();
            var status = form["Status"].FirstOrDefault();
            var managerName = form["ManagerName"].FirstOrDefault();
            var managerMobile = form["ManagerMobile"].FirstOrDefault();
            var managerEmail = form["ManagerEmail"].FirstOrDefault();
            var additionalInfo = form["AdditionalInfo"].FirstOrDefault();
            var type = form["Type"].FirstOrDefault();

            var nextRequestNumber = GetNextAvailableRequestNumber();

            // Build the model
            var request = new EquipmentKitRequestModel
            {
                Id = nextRequestNumber,
                TeamName = teamName ?? "",
                Status = status ?? "To Do",
                ManagerName = managerName ?? "",
                ManagerMobile = managerMobile ?? "",
                ManagerEmail = managerEmail ?? "",
                AdditionalInfo = additionalInfo ?? "",
                Type = type ?? "",
                DateSubmitted = DateTime.UtcNow
            };

            // Save to file
            var fileName = $"equipmentrequest-{nextRequestNumber}.json";
            var filePath = Path.Combine(_directoryPath, fileName);

            var json = JsonConvert.SerializeObject(request, Formatting.Indented);
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

    [HttpGet("EquipmentRequests/LoadRequest/{id}")]
    public IActionResult LoadRequest(int id)
    {
        try
        {
            if (!Directory.Exists(_directoryPath)) return NotFound("Equipment requests directory not found.");

            var filePath = Path.Combine(_directoryPath, $"equipmentrequest-{id}.json");

            if (!System.IO.File.Exists(filePath)) return NotFound("Equipment request not found.");

            var jsonData = System.IO.File.ReadAllText(filePath);
            var request = JsonConvert.DeserializeObject<EquipmentKitRequestModel>(jsonData);

            if (request == null) return NotFound("Equipment request data is invalid.");

            var vm = new KitsRequestsViewModel
            {
                Teams = LoadTeams(),
                ExistingRequest = request // 👈 Pass it to the view
            };

            return View("Index", vm); // Reuse your existing Index view
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error loading kit request: {ex.Message}");
        }
    }


    [HttpGet("EquipmentRequests/Success/{id}")]
    public IActionResult Success(int id)
    {
        return View(id);
    }

    private int GetNextAvailableRequestNumber()
    {
        var existingFiles = Directory.GetFiles(_directoryPath, "equipmentrequest-*.json");
        var highest = 0;

        var regex = new Regex(@"equipmentrequest-(\d+)\.json");

        foreach (var file in existingFiles)
        {
            var match = regex.Match(Path.GetFileName(file));
            if (match.Success && int.TryParse(match.Groups[1].Value, out var num)) highest = Math.Max(highest, num);
        }

        return highest + 1;
    }
}