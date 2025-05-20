using Glenavon.JFC.WebApp.Services;

namespace Glenavon.JFC.WebApp.Controllers;

[Authorize(Roles = "Manager,Admin,SuperAdmin")]
public class EquipmentRequestsController : Controller
{
    private readonly string _directoryPath = "wwwroot/data/equipmentrequests";

    private readonly EmailService _emailService;
    private readonly string _filePath = "wwwroot/data/teams.json";

    public EquipmentRequestsController(EmailService emailService)
    {
        _emailService = emailService;
    }

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
        if (!System.IO.File.Exists(_filePath)) return [];
        var jsonData = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<TeamModel>>(jsonData) ?? [];
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

            var htmlBody = $@"
    <b>Request ID:</b> {request.Id}<br/>
    <b>Team Name:</b> {request.TeamName}<br/>
    <b>Status:</b> {request.Status}<br/>
    <b>Manager Name:</b> {request.ManagerName}<br/>
    <b>Manager Mobile:</b> {request.ManagerMobile}<br/>
    <b>Manager Email:</b> {request.ManagerEmail}<br/>
    <b>Additional Info:</b> {request.AdditionalInfo}<br/>
    <b>Type:</b> {request.Type}<br/>
    <b>Date Submitted:</b> {request.DateSubmitted:dd/MM/yyyy HH:mm}<br/><br/>
    To manage this request, go to <a href='https://www.glenavonjfc.co.uk/EquipmentKitManager'>https://www.glenavonjfc.co.uk/EquipmentKitManager</a>";


            await _emailService.SendEmailAsync("equipmentkitrequests@glenavonjfc.co.uk",
                $"Equipment Request {nextRequestNumber} - {type}", htmlBody);

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