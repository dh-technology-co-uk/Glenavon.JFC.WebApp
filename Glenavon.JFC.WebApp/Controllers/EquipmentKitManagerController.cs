namespace Glenavon.JFC.WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class EquipmentKitManagerController : Controller
{
    private readonly string _directoryPath = "wwwroot/data/kitrequests";

    private List<EquipmentKitRequestModel> ReadItemsFromDirectory()
    {
        var items = new List<EquipmentKitRequestModel>();

        if (!Directory.Exists(_directoryPath))
            return items;

        var files = Directory.GetFiles(_directoryPath, "kitrequest-*.json");

        foreach (var file in files)
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


        // ADD EQUIPMENT REQUESTS HERE


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
        if (!Directory.Exists(_directoryPath))
            Directory.CreateDirectory(_directoryPath);

        var fileName = $"kitrequest-{item.Id}.json";
        var fullPath = Path.Combine(_directoryPath, fileName);
        var jsonData = JsonConvert.SerializeObject(item, Formatting.Indented);
        System.IO.File.WriteAllText(fullPath, jsonData);
    }


    [HttpPost]
    public IActionResult UpdateItem(int id, string status)
    {
        var filePath = Path.Combine(_directoryPath, $"kitrequest-{id}.json");
        if (System.IO.File.Exists(filePath))
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            var item = JsonConvert.DeserializeObject<EquipmentKitRequestModel>(jsonData);
            if (item != null)
            {
                item.Status = status;
                SaveItemToFile(item);
            }
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult DeleteItem(int id)
    {
        var filePath = Path.Combine(_directoryPath, $"kitrequest-{id}.json");
        if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);

        return RedirectToAction("Index");
    }
}