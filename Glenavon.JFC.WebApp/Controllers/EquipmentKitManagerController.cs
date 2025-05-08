namespace Glenavon.JFC.WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class EquipmentKitManagerController : Controller
{
    private readonly string _directoryPathEquipment = "wwwroot/data/equipmentrequests";
    private readonly string _directoryPathKits = "wwwroot/data/kitrequests";

    private List<EquipmentKitRequestModel> ReadItemsFromDirectory()
    {
        var items = new List<EquipmentKitRequestModel>();

        // Read kit requests
        if (Directory.Exists(_directoryPathKits))
        {
            var kitFiles = Directory.GetFiles(_directoryPathKits, "kitrequest-*.json");
            foreach (var file in kitFiles)
                try
                {
                    var jsonData = System.IO.File.ReadAllText(file);
                    var item = JsonConvert.DeserializeObject<EquipmentKitRequestModel>(jsonData);
                    if (item != null)
                    {
                        item.Type = "Kit";
                        items.Add(item);
                    }
                }
                catch
                {
                    // Optionally log or handle invalid JSON files
                }
        }

        // Read equipment requests
        if (Directory.Exists(_directoryPathEquipment))
        {
            var equipmentFiles = Directory.GetFiles(_directoryPathEquipment, "equipmentrequest-*.json");
            foreach (var file in equipmentFiles)
                try
                {
                    var jsonData = System.IO.File.ReadAllText(file);
                    var item = JsonConvert.DeserializeObject<EquipmentKitRequestModel>(jsonData);
                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
                catch
                {
                    // Optionally log or handle invalid JSON files
                }
        }

        return items;
    }


    public IActionResult Index()
    {
        var kits = ReadItemsFromDirectory();

        var viewModel = new KitsManagerViewModel
        {
            RequestsByStatus = new Dictionary<string, List<EquipmentKitRequestModel>>
            {
                { "To Do", kits.Where(k => k.Status == "To Do").ToList() },
                { "In Progress", kits.Where(k => k.Status == "In Progress").ToList() },
                { "Blocked", kits.Where(k => k.Status == "Blocked").ToList() },
                { "Complete", kits.Where(k => k.Status == "Complete").ToList() }
            }
        };

        return View(viewModel);
    }

    private void SaveItemToFile(EquipmentKitRequestModel item)
    {
        var (directory, prefix) = item.Type == "Equipment"
            ? (_directoryPathEquipment, "equipmentrequest")
            : (_directoryPathKits, "kitrequest");

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        var fileName = $"{prefix}-{item.Id}.json";
        var fullPath = Path.Combine(directory, fileName);
        var jsonData = JsonConvert.SerializeObject(item, Formatting.Indented);
        System.IO.File.WriteAllText(fullPath, jsonData);
    }


    [HttpPost]
    public IActionResult UpdateItem(int id, string status, string type)
    {
        var (directory, prefix) = type == "Equipment"
            ? (_directoryPathEquipment, "equipmentrequest")
            : (_directoryPathKits, "kitrequest");

        var filePath = Path.Combine(directory, $"{prefix}-{id}.json");
        if (System.IO.File.Exists(filePath))
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            var item = JsonConvert.DeserializeObject<EquipmentKitRequestModel>(jsonData);
            if (item != null)
            {
                item.Status = status;
                item.Type = type;
                SaveItemToFile(item);
            }
        }

        return RedirectToAction("Index");
    }


    [HttpPost]
    public IActionResult DeleteItem(int id, string type)
    {
        var (directory, prefix) = type == "Equipment"
            ? (_directoryPathEquipment, "equipmentrequest")
            : (_directoryPathKits, "kitrequest");

        var filePath = Path.Combine(directory, $"{prefix}-{id}.json");
        if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);

        return RedirectToAction("Index");
    }
}