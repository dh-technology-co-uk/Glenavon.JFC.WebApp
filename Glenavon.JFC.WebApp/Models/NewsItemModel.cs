namespace Glenavon.JFC.WebApp.Models;

public class NewsItemModel
{
    public string Title { get; set; }
    public string SubHeading { get; set; }
    public string Content { get; set; }
    public string ImagePath { get; set; }
    public DateTime PublishedDate { get; set; }
    public DateTime LastEdited { get; set; }
}