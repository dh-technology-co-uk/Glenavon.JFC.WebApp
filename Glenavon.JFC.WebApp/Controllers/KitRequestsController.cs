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
    public async Task<IActionResult> SubmitTeam()
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            var form = await Request.ReadFormAsync();

            // Extract fields
            var teamName = form["TeamName"].FirstOrDefault();
            var status = form["Status"].FirstOrDefault();
            var managerName = form["ManagerName"].FirstOrDefault();
            var managerMobile = form["ManagerMobile"].FirstOrDefault();
            var managerEmail = form["ManagerEmail"].FirstOrDefault();
            var additionalInfo = form["AdditionalInfo"].FirstOrDefault();
            var playersJson = form["Players"].FirstOrDefault();

            // Deserialize Players JSON
            var players = JsonConvert.DeserializeObject<List<KitItemModel>>(playersJson ?? "[]");

            // Handle Sponsor Logo file (optional)
            byte[]? sponsorLogoBytes = null;
            if (form.Files.Count > 0)
            {
                var sponsorLogoFile = form.Files.GetFile("SponsorLogo");
                if (sponsorLogoFile != null && sponsorLogoFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await sponsorLogoFile.CopyToAsync(ms);
                        sponsorLogoBytes = ms.ToArray();
                    }
                }
            }

            int nextRequestNumber = GetNextAvailableRequestNumber();

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
                Players = players ?? new List<KitItemModel>(),
                SponsorLogo = sponsorLogoBytes
            };

            // Save to file
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

    [HttpGet]
    [Route("KitRequests/LoadTeam/{id}")]
    public IActionResult LoadTeam(int id)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
            {
                return NotFound("Kit requests directory not found.");
            }

            string filePath = Path.Combine(_directoryPath, $"kitrequest-{id}.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Kit request not found.");
            }

            var jsonData = System.IO.File.ReadAllText(filePath);
            var kitRequest = JsonConvert.DeserializeObject<EquipmentKitRequestModel>(jsonData);

            if (kitRequest == null)
            {
                return NotFound("Kit request data is invalid.");
            }

            var vm = new KitsRequestsViewModel
            {
                Teams = LoadTeams(),
                ExistingRequest = kitRequest // 👈 Pass it to the view
            };

            return View("Index", vm); // Reuse your existing Index view
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error loading kit request: {ex.Message}");
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