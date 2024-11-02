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
                    TeamSponsored = "U07 Hawks"
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
                    TeamSponsored = "U09 Hawks"
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
                    TeamSponsored = "U09 Falcons"
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
                    TeamSponsored = "U09 Eagles"
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
                    TeamSponsored = "U08 Kestrels"
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
                    TeamSponsored = "U08 Eagles"
                },
                new()
                {
                    ImagePath = "/images/sponsors/ray-the-roofer.jpg",
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
                    TeamSponsored = "U08 Belles"
                },
                new()
                {
                    ImagePath = "/images/sponsors/ccb-steel.png",
                    Title = "CCB Sheet Steel Ltd",
                    Description =
                        "Steel stockholder supplying UK and Ireland",
                    Address = "204A Washing Bay Road, Dungannon, Co Tyrone, BT71 5EG",
                    Website = "https://www.ccbsheetsteel.co.uk",
                    Email= "alex@ccbsheetsteel.co.uk",
                    ContactName = "Alex Payne",
                    ContactNumber = "07771393646",
                    TeamSponsored = "U07 Hawks"
                },
                new()
                {
                    ImagePath = "/images/sponsors/sean-may-mortgages.png",
                    Title = "Sean May Mortgages",
                    Description =
                        "I provide professional mortgage &amp; protection advice for a wide range of different scenarios, from first time buyers who are just getting onto the property ladder, straight through to professional landlords adding additional Buy to Lets to their portfolio. I can help review your existing mortgage to make sure you have the best possible rate available and also help you release some of the equity in your home for those much needed home improvements.",
                    Address = "Dan",
                    Website = "https://www.seanmaymortgages.co.uk",
                    Email= "sean@seanmaymortgages.co.uk",
                    ContactName = "Sean May",
                    ContactNumber = "07988308624",
                    TeamSponsored = "U09 Eagles"
                },
                new()
                {
                    ImagePath = "/images/sponsors/inciper.jpg",
                    Title = "Inciper",
                    Description =
                        "Inciper was born out of the belief that technology delivery could be done differently. For too long, implementations of Microsoft Business Applications (namely CRM, ERP and Data & Analytics) have been done the same way with customers waiting months and sometimes years to finish an implementation that often did not then deliver the value that was promised at the start. This was particularly true for businesses in the midmarket who were neither large enough to be a priority for the big SIs, nor small enough to avoid the need to implement them altogether. So, we launched our leading Microsoft Business Applications consultancy to drive digital transformation for organisations through best-in-class delivery using our agile RAPID approach. From strategy and advisory to implementation and beyond, with full managed services (aftercare & support), Inciper start and end with your business – evolving your solution after deployment to fortify its future, ensuring you get ongoing measurable value from your technology investment through improved operational efficiency, greater productivity, and deeper insights.",
                    Address = "I Finsbury Avenue, London, EC2M 2PF",
                    Website = "http://www.inciper.com",
                    Email= "Rebecca.hellard@inciper.com",
                    ContactName = "Rebecca Hellard",
                    ContactNumber = "07542841232",
                    TeamSponsored = "U16 Kesterls"

                }
            }
        };


        return View(vm);
    }
}