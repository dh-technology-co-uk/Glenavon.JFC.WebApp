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
                        "A refreshing approach to software consultancy and digital transformation working with you",
                    Address = "1 Gayton Avenue, Higher Bebington, Wirralm CH63 5QB",
                    Email = "dan@dhtechnology.co.uk",
                    ContactName = "Dan Hulmston",
                    ContactNumber = "07932630392",
                    TeamSponsored = "U7 Hawks"
                },
                new()
                {
                    ImagePath = "/images/sponsors/gd_window_cleaning.jpg",
                    Title = "GD Windows Cleaning",
                    Description = "We are a Wirral-based service with over 11 years experience of all types of domestic and commercial exterior cleaning. We specialise in window cleaning and maintenance using the latest pure water-fed pole system. Contact us for a free quote",
       
                },
                new()
                {
                    ImagePath = "/images/sponsors/kitchenware.png",
                    Title = "Kitchenware Express",
                    Description = "Kitchenware Express offers you some of the worlds biggest and best kitchenware brands at market-beating prices. ",
                    //Address = "101 Blockchain Way, Birmingham, B1 1RD",
                    //Email = "sales@futurefintech.co.uk",
                    //ContactName = "Robert Brown",
                    //ContactNumber = "0121 789 0123"
                },
                new()
                {
                    ImagePath = "/images/sponsors/paullavelle.jpg", 
                    Title = "Paul Lavelle Foundation",
                    Description = "We are a charity set up in memory of our mate Paul Lavelle who was sadly taken from us in cruel circumstances in May 2017. After Pauls loss a large group of us (we have a network of 50 mates) decided to honour Paul and set up the charity to raise awareness of domestic abuse towards men.",
                    Address = "The Community Village 330-334 New Chester Road, Birkenhead, CH42 1LE",
                    Website = "https://paullavellefoundation.co.uk",
                    Email = "info@paullavellefoundation.co.uk",
                    ContactName = "Paul Gladwell",
                    ContactNumber = "01512944176",
                    TeamSponsored = "U12 Falcons"
                },
                new()
                {
                    ImagePath = "/images/sponsors/merseyrail.png",
                    Title = "Merseyrail",
                    Description = "Merseyrail is the most punctual and reliable rail network in the UK based on statistics from 2021-22, with consistently high scores for passenger satisfaction. It has received multiple national award wins for customer service and punctuality.",
                    Website = "https://www.merseyrail.org",
                    Email = "sally@merseyrail.org",
                    ContactName = "Sally Ralston",
                    ContactNumber = "01519552142",
                    TeamSponsored = "U15 Falcons"
                },
                new()
                {
                    ImagePath = "/images/sponsors/fortis.png",
                    Title = "Fortis Engineering Services",
                    Description = "Fortis Engineering Services Limited specialise in a range of engineering, fabrication and welding with a highly skilled craftsman who have an excellent track record in Health &amp; Safety.",
                    Address = "Unit 75, Woodside Business Park, Birkenhead, CH411EP",
                    Website = "http://www.fortisengineeringservices.co.uk",
                    Email = "craig@fortises.co.uk",
                    ContactName = "Craig Fletcher",
                    ContactNumber = "01515560554",
                    TeamSponsored = "U9 Hawks"
                },
                new()
                {
                    ImagePath = "/images/sponsors/tates.jpg",
                    Title = "Tates Garages",
                    Description = "We also offer a wide range of servicies including Wheel balancing, tracking, MOT &amp; Air Conditioning",
                    Address = "Pooltown Road, Whitby, Ellesmere Port, CH65 7AB",
                    Website = "https://www.ellesmereport-tyres.co.uk",
                    Email = "tatesgarages@gmail.com",
                    ContactName = "Tony and Vicki Gorman",
                    ContactNumber = "01513562887",
                    TeamSponsored = "U18 Kestrels"
                },
                new()
                {
                    ImagePath = "/images/sponsors/prestige.png",
                    Title = "Prestige",
                    Description = "Cookware manufacturer and housewares distributor",
                    Address = "Meyer Group, Wirral International Business Park, Riverview Road, Bromborough, CH62 3RH",
                    Website = "https://www.prestige.co.uk",
                    Email = "",
                    ContactName = "",
                    ContactNumber = "01514828282",
                    TeamSponsored = "U9 Falcons"
                },
                new()
                {
                    ImagePath = "/images/sponsors/mg-aesthetics.png",
                    Title = "MG Aesthetics Clinic",
                    Description = "MG Aesthetics Clinic takes care of all your aesthetics needs as well as being skin care specialist, we also have spmu specialist and a beautician. Here at MG we pride ourselves on natural results.",
                    Address = "Healthworks, 5 Chadwick Street, Moreton, CH46 7TE",
                    Website = "",
                    Email = "Melissa.lillymae@gmail.com",
                    ContactName = "Melissa Bentley/Gemma Price",
                    ContactNumber = "07710895812",
                    TeamSponsored = "U9 Eagles"
                },
                new()
                {
                    ImagePath = "/images/sponsors/fourth-wall.png",
                    Title = "Fourth Wall Creative",
                    Description = "We are the fan engagement experts. By delivering fresh, tech-led customer engagement solutions, we help clients grow and monetise their customer base.",
                    Address = "2, Riverview Business Park, Shore Wood Rd, Bromborough CH62 3RQ",
                    Website = "https://fourthwallcreative.com",
                    Email = "info@fourthwallcreative.com",
                    ContactName = "",
                    ContactNumber = "01513537310",
                    TeamSponsored = "U8 Kestrels"
                },
                new()
                {
                    ImagePath = "/images/sponsors/bw-cubbins.png",
                    Title = "B. W. Cubbins Ltd",
                    Description = "Building Services",
                    Address = "131 Waterpark Road, Prenton, Merseyside, CH43 0SL",
                    Website = "http://www.bwcubbins.co.uk",
                    Email = "info@bwcubbinsltd.co.uk",
                    ContactName = "Carl Finnigan",
                    ContactNumber = "07921480653",
                    TeamSponsored = "U8 Eagles"
                },
                new()
                {
                    ImagePath = "",
                    Title = "Ray The Roofer",
                    Description = "Roofing Services",
                    Address = "41 Park Road West, Birkenhead, CH43 8SG",
                    Website = "",
                    Email = "raycox1985@gmail.com",
                    ContactName = "Ray Cox",
                    ContactNumber = "07766763699",
                    TeamSponsored = "U12 Eagles"
                },
                new()
                {
                    ImagePath = "/images/sponsors/trainer-cleanse.png",
                    Title = "Trainer Cleanse",
                    Description = "Trainer Cleanse was founded late 2021, providing a luxury cleaning and restoration service for all footwear, with a Collection and Delivery Service throughout The Wirral and the surrounding areas. National Coverage is also provided for footwear sent by post. We offer a professional trainer cleaning service for all trainers brands covering traditional sports brands like Nike, Adidas and New Balance, to designer trainers Dior, Gucci, and Balenciaga.",
                    Address = "",
                    Website = "https://trainer-cleanse.company.site/",
                    Email = "",
                    ContactName = "Phil Bevan",
                    ContactNumber = "07766763699",
                    TeamSponsored = "U16 Hawks"
                },
                new()
                {
                    ImagePath = "/images/sponsors/townfield-pharmacy.jpg",
                    Title = "Townfield Pharmacy",
                    Description = "At Townfield Pharmacy, we strive to provide our patients with the highest quality of service possible. We work very closely with Townfield Surgery next door to ensure patient needs are always met, and their expectations exceeded.",
                    Address = "Townfield Cl, Birkenhead, Prenton CH43 9JW",
                    Website = "https://townfieldpharmacy.co.uk/",
                    Email = "townfieldpharmacy@imaanhealthcare.com",
                    ContactName = "",
                    ContactNumber = "01516537707",
                    TeamSponsored = "U16 Hawks"
                },
                new()
                {
                    ImagePath = "/images/sponsors/salon-supplies-365.jpg",
                    Title = "Salon Supplies 365",
                    Description = "Local stockist of professional hair and beauty products, providing great service and value.",
                    Address = "Unit 8 Badger Way, Prenton, Wirral, CH43 3HQ",
                    Website = "http://www.salonsupplies365.co.uk/",
                    Email = "amy@salonsupplies365.co.uk",
                    ContactName = "Amy Ygartua",
                    ContactNumber = "01513456272",
                    TeamSponsored = "U7 Eagles, U10 Eagles, U10 Kesterls, U13 Eagles"
                },
                new()
                {
                    ImagePath = "/images/sponsors/autosmart.png",
                    Title = "Autosmart",
                    Description = "We Manufacture and Deliver Market-Leading Vehicle-Cleaning Products That Make Our Customers Lives Easier",
                    Address = "Unit 16, Appin Road, CH41 9HH",
                    Website = "http://autosmart.co.uk",
                    Email = "mark.price@autosmart.uk.net",
                    ContactName = "Mark Price",
                    ContactNumber = "07771590767",
                    TeamSponsored = "U8 Belles"
                },
            }
        };


        return View(vm);
    }
}