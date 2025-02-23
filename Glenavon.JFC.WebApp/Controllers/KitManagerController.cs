namespace Glenavon.JFC.WebApp.Controllers;

public class KitManagerController : Controller
{
    private readonly string _filePath = "wwwroot/data/kitmanager.json";

    private List<KitModel> ReadItemsFromJson()
    {
        if (!System.IO.File.Exists(_filePath))
        {
            return new List<KitModel>();
        }

        var jsonData = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<KitModel>>(jsonData) ?? new List<KitModel>();
    }

    private void SaveItemsToJson(List<KitModel> items)
    {
        var jsonData = JsonConvert.SerializeObject(items, Formatting.Indented);
        System.IO.File.WriteAllText(_filePath, jsonData);
    }

    public IActionResult Index()
    {
        var kits = ReadItemsFromJson();

        var viewModel = new KitsManagerViewModel
        {
            KitsByStatus = new Dictionary<string, List<KitModel>>
                {
                    { "To Do", kits.Where(k => k.Status == "To Do").ToList() },
                    { "In Progress", kits.Where(k => k.Status == "In Progress").ToList() },
                    { "Blocked", kits.Where(k => k.Status == "Blocked").ToList() },
                    { "Complete", kits.Where(k => k.Status == "Complete").ToList() }
                }
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult AddItem(KitModel item)
    {
        var items = ReadItemsFromJson();
        item.Id = items.Count > 0 ? items.Max(t => t.Id) + 1 : 1;
        items.Add(item);
        SaveItemsToJson(items);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateItem(int id, string status)
    {
        var items = ReadItemsFromJson();
        var item = items.FirstOrDefault(t => t.Id == id);
        if (item != null)
        {
            item.Status = status;
            SaveItemsToJson(items);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult DeleteItem(int id)
    {
        var items = ReadItemsFromJson();
        items.RemoveAll(t => t.Id == id);
        SaveItemsToJson(items);

        return RedirectToAction("Index");
    }
}