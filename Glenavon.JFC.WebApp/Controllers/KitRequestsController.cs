namespace Glenavon.JFC.WebApp.Controllers;

[Authorize(Roles = "Manager,Admin,SuperAdmin")]
public class KitRequestsController(EmailService emailService) : Controller
{
    private readonly string _directoryPath = "wwwroot/data/kitrequests";
    private readonly string _filePath = "wwwroot/data/teams.json";

    public IActionResult Index()
    {
        var vm = new KitsRequestsViewModel
        {
            Teams = LoadTeams()
        };

        return View(vm);
    }

    private List<TeamModel> LoadTeams()
    {
        if (!System.IO.File.Exists(_filePath)) return [];
        var jsonData = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<TeamModel>>(jsonData) ?? [];
    }

    [HttpPost("KitRequests/SubmitTeam")]
    public async Task<IActionResult> SubmitTeam()
    {
        try
        {
            if (!Directory.Exists(_directoryPath)) Directory.CreateDirectory(_directoryPath);

            var form = await Request.ReadFormAsync();

            // Extract fields
            var teamName = form["TeamName"].FirstOrDefault();
            var status = form["Status"].FirstOrDefault();
            var managerName = form["ManagerName"].FirstOrDefault();
            var managerMobile = form["ManagerMobile"].FirstOrDefault();
            var managerEmail = form["ManagerEmail"].FirstOrDefault();
            var additionalInfo = form["AdditionalInfo"].FirstOrDefault();
            var playersJson = form["Players"].FirstOrDefault();

            // Deserialize Players JSON
            var players = JsonConvert.DeserializeObject<List<KitItemModel>>(playersJson ?? "[]");

            // Handle Sponsor Logo file (optional)
            byte[]? sponsorLogoBytes = null;
            byte[]? sleeveLogoBytes = null;
            if (form.Files.Count > 0)
            {
                var sponsorLogoFile = form.Files.GetFile("SponsorLogo");
                if (sponsorLogoFile != null && sponsorLogoFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await sponsorLogoFile.CopyToAsync(ms);
                    sponsorLogoBytes = ms.ToArray();
                }

                var sleeveLogoFile = form.Files.GetFile("SleeveLogo");
                if (sleeveLogoFile != null && sleeveLogoFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await sleeveLogoFile.CopyToAsync(ms);
                    sleeveLogoBytes = ms.ToArray();
                }
            }

            var nextRequestNumber = GetNextAvailableRequestNumber();

            // Build the model
            var request = new EquipmentKitRequestModel
            {
                Id = nextRequestNumber,
                TeamName = teamName ?? "",
                Status = status ?? "To Do",
                ManagerName = managerName ?? "",
                ManagerMobile = managerMobile ?? "",
                ManagerEmail = managerEmail ?? "",
                AdditionalInfo = additionalInfo ?? "",
                Players = players ?? [],
                DateSubmitted = DateTime.UtcNow,
                SponsorLogo = sponsorLogoBytes,
                SleeveLogo = sleeveLogoBytes
            };

            // Save to file
            var fileName = $"kitrequest-{nextRequestNumber}.json";
            var filePath = Path.Combine(_directoryPath, fileName);

            var json = JsonConvert.SerializeObject(request, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            var htmlBody = $@"
<b>Request ID:</b> {request.Id}<br/>
<b>Team Name:</b> {request.TeamName}<br/>
<b>Manager Name:</b> {request.ManagerName}<br/>
<b>Manager Mobile:</b> {request.ManagerMobile}<br/>
<b>Manager Email:</b> {request.ManagerEmail}<br/>
<b>Additional Info:</b> {request.AdditionalInfo}<br/>
<b>Date Submitted:</b> {request.DateSubmitted:dd/MM/yyyy HH:mm}<br/><br/>
To manage this request, go to <a href='https://www.glenavonjfc.co.uk/EquipmentKitManager'>https://www.glenavonjfc.co.uk/EquipmentKitManager</a>";

            await emailService.SendEmailAsync("equipmentkitrequests@glenavonjfc.co.uk",$"Kit Request {nextRequestNumber}", htmlBody);

            return Ok(new
            {
                success = true,
                requestNumber = nextRequestNumber,
                message = "Team request submitted successfully."
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error saving request: {ex.Message}");
        }
    }

    [HttpGet("KitRequests/LoadRequest/{id}")]
    public IActionResult LoadRequest(int id)
    {
        try
        {
            if (!Directory.Exists(_directoryPath)) return NotFound("Kit requests directory not found.");

            var filePath = Path.Combine(_directoryPath, $"kitrequest-{id}.json");

            if (!System.IO.File.Exists(filePath)) return NotFound("Kit request not found.");

            var jsonData = System.IO.File.ReadAllText(filePath);
            var kitRequest = JsonConvert.DeserializeObject<EquipmentKitRequestModel>(jsonData);

            if (kitRequest == null) return NotFound("Kit request data is invalid.");

            var vm = new KitsRequestsViewModel
            {
                Teams = LoadTeams(),
                ExistingRequest = kitRequest // 👈 Pass it to the view
            };

            vm.ExistingRequest.Type = "Kit";

            return View("Index", vm); // Reuse your existing Index view
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error loading kit request: {ex.Message}");
        }
    }

    [HttpGet("KitRequests/DownloadExcel/{id}")]
    public IActionResult DownloadKitRequestExcel(int id)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
                return NotFound("Kit requests directory not found.");

            var filePath = Path.Combine(_directoryPath, $"kitrequest-{id}.json");

            if (!System.IO.File.Exists(filePath))
                return NotFound("Kit request not found.");

            var jsonData = System.IO.File.ReadAllText(filePath);
            var kitRequest = JsonConvert.DeserializeObject<EquipmentKitRequestModel>(jsonData);

            if (kitRequest == null)
                return NotFound("Kit request data is invalid.");

            // Step 1: Generate Excel with ClosedXML
            var closedXmlStream = new MemoryStream();
            using (var workbook = new XLWorkbook())
            {
                var kitRequestWorksheet = workbook.Worksheets.Add("Kit Request Info");

                var infoFields = new Dictionary<string, object?>
                {
                    { "Kit Request #", kitRequest.Id },
                    { "Team Name", kitRequest.TeamName },
                    { "Manager Name", kitRequest.ManagerName },
                    { "Manager Mobile", kitRequest.ManagerMobile },
                    { "Manager Email", kitRequest.ManagerEmail },
                    { "Additional Info", kitRequest.AdditionalInfo }
                };

                var row = 1;
                foreach (var entry in infoFields)
                {
                    kitRequestWorksheet.Cell(row, 1).Value = entry.Key;
                    kitRequestWorksheet.Cell(row, 2).Value = entry.Value?.ToString() ?? string.Empty;
                    row++;
                }

                // ADD BLANK ROW AFTER INFO
                row++;

                // PLAYER TITLE
                kitRequestWorksheet.Cell(row, 1).Value = "Player Kits";
                row++;

                kitRequestWorksheet.Cell(row, 1).Value = "Top Size";
                kitRequestWorksheet.Cell(row, 2).Value = "Shirt Number";
                kitRequestWorksheet.Cell(row, 3).Value = "Shorts Size";
                kitRequestWorksheet.Cell(row, 4).Value = "Socks Size";
                kitRequestWorksheet.Cell(row, 5).Value = "Kit Type";
                kitRequestWorksheet.Cell(row, 6).Value = "Quarter Zip";
                kitRequestWorksheet.Cell(row, 7).Value = "New Player?";
                kitRequestWorksheet.Cell(row, 8).Value = "First Name";
                kitRequestWorksheet.Cell(row, 9).Value = "Surname";
                row++;

                var sizeOrder = new List<string>
                {
                    "XSJ", "SJ", "MJ", "LJ", "XLJ",
                    "S", "M", "L", "XL", "2XL", "3XL",
                    "6", "8", "10", "12", "14", "16", "18"
                };

                foreach (var player in (kitRequest.Players ?? []).OrderBy(p => sizeOrder.IndexOf(p.TopSize))
                         .ThenBy(p => p.ShirtNumber))
                {
                    kitRequestWorksheet.Cell(row, 1).Value = player.TopSize;
                    kitRequestWorksheet.Cell(row, 2).Value = player.ShirtNumber;
                    kitRequestWorksheet.Cell(row, 3).Value = player.ShortsSize;
                    kitRequestWorksheet.Cell(row, 4).Value = player.SocksSize;
                    kitRequestWorksheet.Cell(row, 5).Value = player.KitType;
                    kitRequestWorksheet.Cell(row, 6).Value = player.QuarterZip;
                    kitRequestWorksheet.Cell(row, 7).Value = player.NewPlayer;
                    kitRequestWorksheet.Cell(row, 8).Value = player.FirstName;
                    kitRequestWorksheet.Cell(row, 9).Value = player.Surname;
                    row++;
                }

                workbook.SaveAs(closedXmlStream);
            }

            closedXmlStream.Position = 0;
            return File(closedXmlStream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"kitrequest-{id}.xlsx");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error generating Excel file: {ex.Message}");
        }
    }

    [HttpGet("KitRequests/DownloadSponsorsPdf/{id}")]
    public IActionResult DownloadSponsorLogoPdf(int id)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
                return NotFound("Kit requests directory not found.");

            var filePath = Path.Combine(_directoryPath, $"kitrequest-{id}.json");

            if (!System.IO.File.Exists(filePath))
                return NotFound("Kit request not found.");

            var jsonData = System.IO.File.ReadAllText(filePath);
            var kitRequest = JsonConvert.DeserializeObject<EquipmentKitRequestModel>(jsonData);

            if (kitRequest?.SponsorLogo == null || kitRequest.SponsorLogo.Length == 0)
                return NotFound("No sponsor logo available.");

            // Detect image format and save as PNG
            using var msImage = new MemoryStream();
            try
            {
                using var imageStream = new MemoryStream(kitRequest.SponsorLogo);
                using var image = Image.Load(imageStream, out var format);
                image.Save(msImage, new PngEncoder()); // Re-encode as PNG regardless of original format
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sponsor logo is not a valid image. {ex.Message}");
            }


            msImage.Seek(0, SeekOrigin.Begin);
            // Generate PDF
            using var pdfStream = new MemoryStream();
            var document = new PdfDocument();
            var page = document.AddPage();
            using var gfx = XGraphics.FromPdfPage(page);

            using var imageCopy = new MemoryStream(msImage.ToArray()); // clone for PdfSharpCore
            using var xImage = XImage.FromStream(() => imageCopy);

            double pageWidth = page.Width;
            var aspectRatio = xImage.PixelHeight / (double)xImage.PixelWidth;
            var imageHeight = pageWidth * aspectRatio;

            gfx.DrawImage(xImage, 0, 0, pageWidth, imageHeight);

            document.Save(pdfStream, false);

            return File(pdfStream.ToArray(), "application/pdf", $"kitrequest-{id}-sponsor-logo.pdf");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error generating PDF file: {ex.Message}");
        }
    }

    [HttpGet("KitRequests/DownloadSleevePdf/{id}")]
    public IActionResult DownloadSleeveLogoPdf(int id)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
                return NotFound("Kit requests directory not found.");

            var filePath = Path.Combine(_directoryPath, $"kitrequest-{id}.json");

            if (!System.IO.File.Exists(filePath))
                return NotFound("Kit request not found.");

            var jsonData = System.IO.File.ReadAllText(filePath);
            var kitRequest = JsonConvert.DeserializeObject<EquipmentKitRequestModel>(jsonData);

            if (kitRequest?.SleeveLogo == null || kitRequest.SleeveLogo.Length == 0)
                return NotFound("No sleeve logo available.");

            // Detect image format and save as PNG
            using var msImage = new MemoryStream();
            try
            {
                using var imageStream = new MemoryStream(kitRequest.SleeveLogo);
                using var image = Image.Load(imageStream, out var format);
                image.Save(msImage, new PngEncoder()); // Re-encode as PNG regardless of original format
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sleeve logo is not a valid image. {ex.Message}");
            }

            msImage.Seek(0, SeekOrigin.Begin);

            // Generate PDF
            using var pdfStream = new MemoryStream();
            var document = new PdfDocument();
            var page = document.AddPage();
            using var gfx = XGraphics.FromPdfPage(page);

            using var imageCopy = new MemoryStream(msImage.ToArray()); // clone for PdfSharpCore
            using var xImage = XImage.FromStream(() => imageCopy);

            double pageWidth = page.Width;
            var aspectRatio = xImage.PixelHeight / (double)xImage.PixelWidth;
            var imageHeight = pageWidth * aspectRatio;

            gfx.DrawImage(xImage, 0, 0, pageWidth, imageHeight);

            document.Save(pdfStream, false);

            return File(pdfStream.ToArray(), "application/pdf", $"kitrequest-{id}-sleeve-logo.pdf");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error generating PDF file: {ex.Message}");
        }
    }

    [HttpGet("KitRequests/DownloadInvoice")]
    public IActionResult DownloadInvoiceDocument()
    {
        try
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents", "kitinvoice.docx");

            if (!System.IO.File.Exists(filePath))
                return NotFound("Invoice document not found.");

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            const string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            const string fileName = "kitinvoice.docx";

            return File(fileBytes, contentType, fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error downloading document: {ex.Message}");
        }
    }


    [HttpGet("KitRequests/Success/{id}")]
    public IActionResult Success(int id)
    {
        return View(id);
    }


    private int GetNextAvailableRequestNumber()
    {
        var existingFiles = Directory.GetFiles(_directoryPath, "kitrequest-*.json");
        var highest = 0;

        var regex = new Regex(@"kitrequest-(\d+)\.json");

        foreach (var file in existingFiles)
        {
            var match = regex.Match(Path.GetFileName(file));
            if (match.Success && int.TryParse(match.Groups[1].Value, out var num)) highest = Math.Max(highest, num);
        }

        return highest + 1;
    }
}