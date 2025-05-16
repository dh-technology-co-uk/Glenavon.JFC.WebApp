using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Glenavon.JFC.WebApp.Controllers;

[Authorize(Roles = "Admin,SuperAdmin")]
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
            foreach (var file in Directory.GetFiles(_directoryPathKits, "kitrequest-*.json"))
            {
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
                catch { /* Log or ignore invalid files */ }
            }
        }

        // Read equipment requests
        if (Directory.Exists(_directoryPathEquipment))
        {
            foreach (var file in Directory.GetFiles(_directoryPathEquipment, "equipmentrequest-*.json"))
            {
                try
                {
                    var jsonData = System.IO.File.ReadAllText(file);
                    var item = JsonConvert.DeserializeObject<EquipmentKitRequestModel>(jsonData);
                    if (item != null)
                        items.Add(item);
                }
                catch { /* Log or ignore invalid files */ }
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
        var (directory, prefix) = item.Type.ToLower() == "kit"
            ? (_directoryPathKits, "kitrequest")
            : (_directoryPathEquipment, "equipmentrequest");

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        var fileName = $"{prefix}-{item.Id}.json";
        var fullPath = Path.Combine(directory, fileName);
        var jsonData = JsonConvert.SerializeObject(item, Formatting.Indented);
        System.IO.File.WriteAllText(fullPath, jsonData);
    }

    // ✅ Accept JSON from SortableJS
    public class ItemStatusUpdateModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }

    [HttpPost]
    public IActionResult UpdateItem([FromBody] ItemStatusUpdateModel model)
    {
        if (model == null || model.Id == 0 || string.IsNullOrEmpty(model.Type) || string.IsNullOrEmpty(model.Status))
            return BadRequest("Invalid data");

        var (directory, prefix) = model.Type.ToLower() == "kit"
            ? (_directoryPathKits, "kitrequest")
            : (_directoryPathEquipment, "equipmentrequest");

        var filePath = Path.Combine(directory, $"{prefix}-{model.Id}.json");

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        var jsonData = System.IO.File.ReadAllText(filePath);
        var item = JsonConvert.DeserializeObject<EquipmentKitRequestModel>(jsonData);

        if (item == null)
            return BadRequest("Failed to deserialize item");

        item.Status = model.Status;
        item.Type = model.Type;

        SaveItemToFile(item);

        return Ok("Status updated");
    }

    [HttpPost]
    public IActionResult DeleteItem(int id, string type)
    {
        var (directory, prefix) = type.ToLower() == "kit"
            ? (_directoryPathKits, "kitrequest")
            : (_directoryPathEquipment, "equipmentrequest");

        var filePath = Path.Combine(directory, $"{prefix}-{id}.json");
        if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);

        return RedirectToAction("Index");
    }
}
