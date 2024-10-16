namespace Glenavon.JFC.WebApp.Controllers;

public class VenuesController : Controller
{
    public IActionResult Index()
    {
        VenuesViewModel vm = new()
        {
            Venues = new List<VenueModel>
            {
                new()
                {
                    Name = "The Glen",
                    Address = new AddressModel
                    {
                        AddressLine1 = "The Glen",
                        AddressLine2 = "Woodchurch Road",
                        TownOrCity = "Prenton",
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
                    Rules = new List<RulesModel>
                    {
                        new()
                        {
                            Id = 1,
                            Description = "test"
                        },
                        new()
                        {
                            Id = 2,
                            Description = "test"
                        }
                    }
                },
                new()
                {
                    Name = "The Glen",
                    Address = new AddressModel
                    {
                        AddressLine1 = "The Glen",
                        AddressLine2 = "Woodchurch Road",
                        TownOrCity = "Prenton",
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
                    Rules = new List<RulesModel>
                    {
                        new()
                        {
                            Id = 1,
                            Description = "test"
                        },
                        new()
                        {
                            Id = 2,
                            Description = "test"
                        }
                    }
                },
                new()
                {
                    Name = "The Glen",
                    Address = new AddressModel
                    {
                        AddressLine1 = "The Glen",
                        AddressLine2 = "Woodchurch Road",
                        TownOrCity = "Prenton",
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
                    Rules = new List<RulesModel>
                    {
                        new()
                        {
                            Id = 1,
                            Description = "test"
                        },
                        new()
                        {
                            Id = 2,
                            Description = "test"
                        }
                    }
                },
                new()
                {
                    Name = "The Glen",
                    Address = new AddressModel
                    {
                        AddressLine1 = "The Glen",
                        AddressLine2 = "Woodchurch Road",
                        TownOrCity = "Prenton",
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
                    Rules = new List<RulesModel>
                    {
                        new()
                        {
                            Id = 1,
                            Description = "test"
                        },
                        new()
                        {
                            Id = 2,
                            Description = "test"
                        }
                    }
                }
            }
        };

        return View(vm);
    }
}