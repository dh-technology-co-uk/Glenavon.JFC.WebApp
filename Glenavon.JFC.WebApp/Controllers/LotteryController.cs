namespace Glenavon.JFC.WebApp.Controllers;

public class LotteryController : Controller
{
    // Action to serve the view that contains the embedded external page
    public IActionResult Index()
    {
        return View("Index");
    }

    public async Task<IActionResult> ExternalPage(string url = "https://yourlottery.org/glenavon/")
    {
        url = Uri.IsWellFormedUriString(url, UriKind.Absolute) ? url : "https://yourlottery.org/glenavon/";

        try
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(response);

            // Rewrite all anchor tags to use the RedirectLink action
            foreach (var link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                var href = link.GetAttributeValue("href", "");
                if (!href.StartsWith("http")) href = new Uri(new Uri(url), href).ToString();
                link.SetAttributeValue("href", Url.Action("RedirectLink", "Lottery", new { targetUrl = href }));
            }

            // Ensure all images have absolute URLs
            foreach (var img in doc.DocumentNode.SelectNodes("//img[@src]"))
            {
                var src = img.GetAttributeValue("src", "");
                if (!src.StartsWith("http")) src = new Uri(new Uri(url), src).ToString();
                img.SetAttributeValue("src", src);
            }

            // Ensure all stylesheets have absolute URLs
            foreach (var cssNode in doc.DocumentNode.SelectNodes("//link[@rel='stylesheet']"))
            {
                var href = cssNode.GetAttributeValue("href", "");
                if (!href.StartsWith("http")) href = new Uri(new Uri(url), href).ToString();
                cssNode.SetAttributeValue("href", href);
            }

            var modifiedHtml = doc.DocumentNode.OuterHtml;
            return Content(modifiedHtml, "text/html");
        }
        catch (Exception ex)
        {
            return Content($"Error loading external page: {ex.Message}");
        }
    }


    public IActionResult RedirectLink(string targetUrl)
    {
        // Check if the target URL contains a '#' (menu link or in-page anchor)
        if (!string.IsNullOrEmpty(targetUrl) && !targetUrl.Contains("#"))
            // Log the click if needed, and then redirect to the target URL
            return Redirect(targetUrl);

        // If the URL contains a #, return JavaScript to do nothing
        return Content("<script>/* Anchor link, no redirect */</script>", "text/html");
    }
}