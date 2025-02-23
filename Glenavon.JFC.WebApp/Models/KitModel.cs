namespace Glenavon.JFC.WebApp.Models;

public class KitModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } // "Available", "In Use", "Maintenance"
    public DateTime CreatedAt { get; set; }
}