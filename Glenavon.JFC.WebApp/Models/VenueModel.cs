namespace Glenavon.JFC.WebApp.Models;

public class VenueModel
{
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public AddressModel Address { get; set; }
    public ParkingModel Parking { get; set; }
    public List<string> Rules { get; set; }
    public PitchLocationModel PitchLocation { get; set; }
}