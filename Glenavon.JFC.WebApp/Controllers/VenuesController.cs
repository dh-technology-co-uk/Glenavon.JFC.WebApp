namespace Glenavon.JFC.WebApp.Controllers;

public class VenuesController : Controller
{
    public IActionResult Index()
    {
        VenuesViewModel vm = new()
        {
            Venues =
            [
                new VenueModel
                {
                    Name = "The Glen",
                    ImagePath = "/images/venues/the_glen.jpeg",
                    Address = new AddressModel
                    {
                        AddressLine1 = "The Glen",
                        AddressLine2 = "Woodchurch Road",
                        TownOrCity = "Prenton",
                        County = "Wirral",
                        Postcode = "CH43 3AP"
                    },
                    Parking = new ParkingModel
                    {
                        Description =
                            "From M53 junction 3, exit onto Woodchurch Road heading east towards Birkenhead. When M53 slip meets Woodchurch Road the entrance is on the left before the railway bridge. \r\n\r\nPlease be mindful that this junction is off a wide 4 lane carriageway. For safety, we ask all visitors to enter by entering by turning left on and exiting turning left only.\r\n\r\nPlease park your car considerately and DO NOT block others in."
                    },
                    PitchLocation = new PitchLocationModel
                    {
                        Description =
                            "The pitch is approximately 50m north west of the parking. You will access the pitches via the small bridge over the Fender."
                    },
                    Rules =
                    [
                        "No dogs allowed on the pitch.",
                        "Dogs must be kept on leads at all times and dirt must be picked up.",
                        "No alcohol is allowed to be drank pitch side."
                    ]
                },

                new VenueModel
                {
                    Name = "The Solly",
                    ImagePath = "/images/venues/the_solly.jpeg",
                    Address = new AddressModel
                    {
                        AddressLine1 = "The Solly",
                        AddressLine2 = "Woodchurch Road",
                        TownOrCity = "Prenton",
                        County = "Wirral",
                        Postcode = "CH43 3AP"
                    },
                    Parking = new ParkingModel
                    {
                        Description =
                            "From M53 junction 3, exit onto Woodchurch Road heading east towards Birkenhead. When M53 slip meets Woodchurch Road the entrance is on the left before the railway bridge. \r\n\r\nPlease be mindful that this junction is off a wide 4 lane carriageway. For safety, we ask all visitors to enter by entering by turning left on and exiting turning left only.\r\n\r\nPlease park your car considerately and DO NOT block others in."
                    },
                    PitchLocation = new PitchLocationModel
                    {
                        Description =
                            "Once parked, walk onto Woodchurch Road and head east, walking under the railway bridge before immediately turning left onto a side lane. The entrance to the Solly is at the end of this lane."
                    },
                    Rules =
                    [
                        "No dogs allowed on the pitch.",
                        "Dogs must be kept on leads at all times and dirt must be picked up.",
                        "No alcohol is allowed to be drank pitch side."
                    ]
                },

                new VenueModel
                {
                    Name = "Ridgeway High School",
                    ImagePath = "/images/venues/ridgeway.jpeg",
                    Address = new AddressModel
                    {
                        AddressLine1 = "Ridgeway High School",
                        AddressLine2 = "Noctorum Avenue",
                        TownOrCity = "Prenton",
                        County = "Wirral",
                        Postcode = "CH43 9EB"
                    },
                    Parking = new ParkingModel
                    {
                        Description =
                            "Enter the school car park and head to the right."
                    },
                    PitchLocation = new PitchLocationModel
                    {
                        Description =
                            "The pitches are accessed via a gate from the right-hand car park."
                    },
                    Rules =
                    [
                        "No dogs are allowed within the school grounds.",
                        "No alcohol is allowed within the school grounds.",
                        "No Smoking is allowed within the school grounds."
                    ]
                },

                new VenueModel
                {
                    Name = "Woodchurch High Sports Complex",
                    ImagePath = "/images/venues/woodchurch_high.jpeg",
                    Address = new AddressModel
                    {
                        AddressLine1 = "Woodchurch High Sports Complex",
                        AddressLine2 = "Carr Bridge Road",
                        TownOrCity = "Woodchurch",
                        County = "Wirral",
                        Postcode = "CH49 7NG"
                    },
                    Parking = new ParkingModel
                    {
                        Description =
                            "On site"
                    },
                    PitchLocation = new PitchLocationModel
                    {
                        Description =
                            "To rear of the sports centre building."
                    },
                    Rules =
                    [
                        "No dogs are allowed within the sports complex grounds",
                        "No alcohol is allowed within the sports complex grounds",
                        "No alcohol is allowed within the sports complex grounds"
                    ]
                },

                new VenueModel
                {
                    Name = "Woodchurch Community Football Hub",
                    ImagePath = "/images/venues/woodchurch_football_hub.jpg",
                    Address = new AddressModel
                    {
                        AddressLine1 = "Woodchurch Community Football Hub",
                        AddressLine2 = "Carr Bridge Road",
                        TownOrCity = "Woodchurch",
                        County = "Wirral",
                        Postcode = "CH49 8EU"
                    },
                    Parking = new ParkingModel
                    {
                        Description =
                            "On site"
                    },
                    PitchLocation = new PitchLocationModel
                    {
                        Description =
                            "To the right of the community building."
                    },
                    Rules =
                    [
                        "No dogs are allowed within the football hub grounds",
                        "No alcohol is allowed within the football hub grounds",
                        "No alcohol is allowed within the football hub grounds"
                    ]
                }
            ]
        };

        return View(vm);
    }
}