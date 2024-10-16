namespace Glenavon.JFC.WebApp.Models;

public class VenueModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public AddressModel Address { get; set; }
    public ParkingModel Parking { get; set; }
    public List<RulesModel> Rules { get; set; }
    public PitchLocationModel PitchLocation { get; set; }
}