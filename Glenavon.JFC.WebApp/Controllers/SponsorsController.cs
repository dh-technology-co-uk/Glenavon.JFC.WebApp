namespace Glenavon.JFC.WebApp.Controllers;

public class SponsorsController : Controller
{
    public IActionResult Index()
    {
        var vm = new SponsorsViewModel
        {
            Sponsors = new List<SponsorModel>
            {
                new()
                {
                    ImagePath = "/images/sponsors/dhtechnologyltd.png",
                    Title = "DH Technology Ltd",
                    Description =
                        "Window cleaning service with 11 years experience covering Wirral. We use the latest pure water pole.",
                    Address = "123 Tech Road, London, EC1A 1BB",
                    Email = "dan@dhtechnology.co.uk",
                    ContactName = "Dan Hulmston",
                    ContactNumber = "020 7946 0018",
                    TeamSponsored = "U7 Hawks"
                },
                new()
                {
                    ImagePath = "/images/sponsors/gd_window_cleaning.jpeg",
                    Title = "GD Windows Cleaning",
                    Description = "Window cleaning service with 11 years experience covering Wirral. We use the latest pure water pole.",
                    Address = "456 Eco Street, Edinburgh, EH1 1YZ",
                    Email = "info@greenenergy.co.uk",
                    ContactName = "Jane Smith",
                    ContactNumber = "0131 456 7890"
                },
                new()
                {
                    ImagePath = "/images/sponsors/halliday.jpeg",
                    Title = "Halliday Funeral Supplies",
                    Description =
                        "Remaining a family-run business, our core values are committed to being the UKs leading and most reliable and efficient distributor. Whilst having the infrustructure and investment to fulfill our large distribution network, we also offer an emergency service in order to support funeral directors in more urgent situations. We proudly remain one of the largest producers of FSC certified timber products in the UK funeral industry.",
                    Address = "789 Wellness Avenue, Manchester, M1 2JW",
                    Email = "support@healthylivingco.co.uk",
                    ContactName = "Alice Johnson",
                    ContactNumber = "0161 234 5678"
                },
                new()
                {
                    ImagePath = "/images/sponsors/kitchenware.png",
                    Title = "Kitchenware Express",
                    Description = "Kitchenware Express offers you some of the worlds biggest and best kitchenware brands at market-beating prices. ",
                    Address = "101 Blockchain Way, Birmingham, B1 1RD",
                    Email = "sales@futurefintech.co.uk",
                    ContactName = "Robert Brown",
                    ContactNumber = "0121 789 0123"
                },
                new()
                {
                    ImagePath = "/images/sponsor5.png", 
                    Title = "EcoHome Builders",
                    Description = "Building sustainable homes for the future.",
                    Address = "202 Greenfield Lane, Cardiff, CF10 1AW",
                    Email = "contact@ecohomebuilders.co.uk",
                    ContactName = "Laura Williams",
                    ContactNumber = "029 2044 1234"
                },
                new()
                {
                    ImagePath = "/images/sponsor6.png",
                    Title = "Bright Future Solutions",
                    Description = "Innovating the future with sustainable tech.",
                    Address = "45 Tech Road, Cambridge, CB1 1BB",
                    Email = "info@brightfuture.co.uk",
                    ContactName = "Charlie Green",
                    ContactNumber = "01223 456 789"
                },
                new()
                {
                    ImagePath = "/images/sponsor7.png",
                    Title = "Smart Tech Industries",
                    Description = "Developing smarter technology for a better tomorrow.",
                    Address = "78 Innovation Park, Oxford, OX1 3PQ",
                    Email = "support@smarttech.com",
                    ContactName = "Lucy Williams",
                    ContactNumber = "01865 789 123"
                },
                new()
                {
                    ImagePath = "/images/sponsor8.png",
                    Title = "EcoWorld Developments",
                    Description = "Leading sustainable construction and eco-friendly projects.",
                    Address = "32 Green Way, Bristol, BS1 6HG",
                    Email = "contact@ecoworld.co.uk",
                    ContactName = "Mark Johnson",
                    ContactNumber = "0117 456 3210"
                }
            }
        };


        return View(vm);
    }
}