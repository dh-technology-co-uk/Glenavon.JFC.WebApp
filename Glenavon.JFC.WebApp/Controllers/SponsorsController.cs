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
                    Id = 1, ImagePath = "/images/sponsors/dhtechnologyltd.png", Title = "DH Technology Ltd",
                    Description = "Leading provider of tech solutions.",
                    Address = "123 Tech Road, London, EC1A 1BB",
                    Email = "dan@dhtechnology.co.uk",
                    ContactName = "Dan Hulmston",
                    ContactNumber = "020 7946 0018",
                    TeamSponsored = "U7 Hawks"
                },
                new()
                {
                    Id = 2, ImagePath = "/images/sponsor2.png", Title = "Green Energy",
                    Description = "Pioneers in renewable energy.",
                    Address = "456 Eco Street, Edinburgh, EH1 1YZ",
                    Email = "info@greenenergy.co.uk",
                    ContactName = "Jane Smith",
                    ContactNumber = "0131 456 7890"
                },
                new()
                {
                    Id = 3, ImagePath = "/images/sponsor3.png", Title = "Healthy Living Co.",
                    Description = "Providing health products for a better life.",
                    Address = "789 Wellness Avenue, Manchester, M1 2JW",
                    Email = "support@healthylivingco.co.uk",
                    ContactName = "Alice Johnson",
                    ContactNumber = "0161 234 5678"
                },
                new()
                {
                    Id = 4, ImagePath = "/images/sponsor4.png", Title = "Future FinTech",
                    Description = "Shaping the future of financial technology.",
                    Address = "101 Blockchain Way, Birmingham, B1 1RD",
                    Email = "sales@futurefintech.co.uk",
                    ContactName = "Robert Brown",
                    ContactNumber = "0121 789 0123"
                },
                new()
                {
                    Id = 5, ImagePath = "/images/sponsor5.png", Title = "EcoHome Builders",
                    Description = "Building sustainable homes for the future.",
                    Address = "202 Greenfield Lane, Cardiff, CF10 1AW",
                    Email = "contact@ecohomebuilders.co.uk",
                    ContactName = "Laura Williams",
                    ContactNumber = "029 2044 1234"
                },
                new()
                {
                    Id = 6,
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
                    Id = 7,
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
                    Id = 8,
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