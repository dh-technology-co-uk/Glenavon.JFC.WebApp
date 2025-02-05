namespace Glenavon.JFC.WebApp.Controllers;

public class NewsController : Controller
{
    private readonly string _filePath = "wwwroot/data/news.json";

    public IActionResult Index()
    {
        var newsList = LoadNews();
        var viewModel = new NewsViewModel { NewsItems = newsList };
        return View(viewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(NewsItemModel newsItem)
    {
        if (ModelState.IsValid)
        {
            var newsList = LoadNews();
            newsItem.PublishedDate = DateTime.Now;
            newsItem.LastEdited = DateTime.Now;
            newsList.Add(newsItem);
            SaveNews(newsList);
            return RedirectToAction("Index");
        }
        return View(newsItem);
    }

    public IActionResult Edit(int id)
    {
        var newsList = LoadNews();
        var newsItem = newsList.ElementAtOrDefault(id);
        if (newsItem == null) return NotFound();
        return View(newsItem);
    }

    [HttpPost]
    public IActionResult Edit(int id, NewsItemModel updatedNewsItem)
    {
        var newsList = LoadNews();
        if (id < 0 || id >= newsList.Count) return NotFound();
        updatedNewsItem.PublishedDate = newsList[id].PublishedDate;
        updatedNewsItem.LastEdited = DateTime.Now;
        newsList[id] = updatedNewsItem;
        SaveNews(newsList);
        return RedirectToAction("Index");
    }

    private List<NewsItemModel> LoadNews()
    {
        if (!System.IO.File.Exists(_filePath)) return new List<NewsItemModel>();
        var jsonData = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<NewsItemModel>>(jsonData) ?? new List<NewsItemModel>();
    }

    private void SaveNews(List<NewsItemModel> newsList)
    {
        var jsonData = JsonConvert.SerializeObject(newsList, Formatting.Indented);
        System.IO.File.WriteAllText(_filePath, jsonData);
    }
}
