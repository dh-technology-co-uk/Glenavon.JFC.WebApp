namespace Glenavon.JFC.WebApp.Models;

public class AddressModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string TownOrCity { get; set; }

    public string County { get; set; }

    public string Postcode { get; set; }
    public int VenueId { get; set; }
}