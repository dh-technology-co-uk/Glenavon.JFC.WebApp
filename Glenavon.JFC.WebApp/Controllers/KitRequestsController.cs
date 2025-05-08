using System.Text.RegularExpressions;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;


namespace Glenavon.JFC.WebApp.Controllers;

[Authorize(Roles = "Manager,Admin")]
public class KitRequestsController : Controller
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
        if (!System.IO.File.Exists(_filePath)) return new List<TeamModel>();
        var jsonData = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<TeamModel>>(jsonData) ?? new List<TeamModel>();
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
            if (form.Files.Count > 0)
            {
                var sponsorLogoFile = form.Files.GetFile("SponsorLogo");
                if (sponsorLogoFile != null && sponsorLogoFile.Length > 0)
                    using (var ms = new MemoryStream())
                    {
                        await sponsorLogoFile.CopyToAsync(ms);
                        sponsorLogoBytes = ms.ToArray();
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
                Players = players ?? new List<KitItemModel>(),
                DateSubmitted = DateTime.UtcNow,
                SponsorLogo = sponsorLogoBytes
            };

            // Save to file
            var fileName = $"kitrequest-{nextRequestNumber}.json";
            var filePath = Path.Combine(_directoryPath, fileName);

            var json = JsonConvert.SerializeObject(request, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

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

    [HttpGet("KitRequests/LoadTeam/{id}")]
    public IActionResult LoadTeam(int id)
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
                kitRequestWorksheet.Cell(row, 2).Value = "Shorts Size";
                kitRequestWorksheet.Cell(row, 3).Value = "Socks Size";
                kitRequestWorksheet.Cell(row, 4).Value = "Shirt Number";
                kitRequestWorksheet.Cell(row, 5).Value = "Kit Type";
                kitRequestWorksheet.Cell(row, 6).Value = "Quarter Zip";
                row++;

                foreach (var player in kitRequest.Players ?? new List<KitItemModel>())
                {
                    kitRequestWorksheet.Cell(row, 1).Value = player.TopSize;
                    kitRequestWorksheet.Cell(row, 2).Value = player.ShortsSize;
                    kitRequestWorksheet.Cell(row, 3).Value = player.SocksSize;
                    kitRequestWorksheet.Cell(row, 4).Value = player.ShirtNumber;
                    kitRequestWorksheet.Cell(row, 5).Value = player.KitType;
                    kitRequestWorksheet.Cell(row, 6).Value = player.QuarterZip;
                    row++;
                }

                workbook.SaveAs(closedXmlStream);
            }

            // Step 2: Add logo with OpenXML
            closedXmlStream.Position = 0;
            var finalStream = new MemoryStream();
            closedXmlStream.CopyTo(finalStream);
            finalStream.Position = 0;

            if (kitRequest.SponsorLogo.Length > 0)
            {
                using var document = SpreadsheetDocument.Open(finalStream, true);
                var workbookPart = document.WorkbookPart!;
                var logoSheetPart = workbookPart.AddNewPart<WorksheetPart>();
                logoSheetPart.Worksheet = new Worksheet(new SheetData());

                var sheetId = (uint)(workbookPart.Workbook.Sheets.Count() + 1);
                var sheet = new Sheet
                {
                    Id = workbookPart.GetIdOfPart(logoSheetPart),
                    SheetId = sheetId,
                    Name = "Sponsor Logo"
                };
                workbookPart.Workbook.Sheets.Append(sheet);

                // Add drawing part and reference to worksheet
                var drawingsPart = logoSheetPart.AddNewPart<DrawingsPart>();
                logoSheetPart.Worksheet.Append(new Drawing { Id = logoSheetPart.GetIdOfPart(drawingsPart) });
                logoSheetPart.Worksheet.Save();

                // Add image to drawing part
                var imagePart = drawingsPart.AddImagePart(ImagePartType.Png);
                var safeLogoCopy = new MemoryStream(kitRequest.SponsorLogo.ToArray());
                imagePart.FeedData(safeLogoCopy);

                // Build drawing markup
                var worksheetDrawing = new Xdr.WorksheetDrawing();
                var picture = new Xdr.Picture(
                    new Xdr.NonVisualPictureProperties(
                        new Xdr.NonVisualDrawingProperties { Id = 1U, Name = "SponsorLogo.png" },
                        new Xdr.NonVisualPictureDrawingProperties()
                    ),
                    new Xdr.BlipFill(
                        new A.Blip
                        {
                            Embed = drawingsPart.GetIdOfPart(imagePart),
                            CompressionState = A.BlipCompressionValues.Print
                        },
                        new A.Stretch(new A.FillRectangle())
                    ),
                    new Xdr.ShapeProperties(
                        new A.Transform2D(
                            new A.Offset { X = 0L, Y = 0L },
                            new A.Extents { Cx = 5000000L, Cy = 3000000L } // Size in EMUs
                        ),
                        new A.PresetGeometry(new A.AdjustValueList()) { Preset = A.ShapeTypeValues.Rectangle }
                    )
                );

                var anchor = new Xdr.TwoCellAnchor(
                    new Xdr.FromMarker(
                        new Xdr.ColumnId("1"), new Xdr.ColumnOffset("0"),
                        new Xdr.RowId("1"), new Xdr.RowOffset("0")),
                    new Xdr.ToMarker(
                        new Xdr.ColumnId("10"), new Xdr.ColumnOffset("0"),
                        new Xdr.RowId("20"), new Xdr.RowOffset("0")),
                    picture,
                    new Xdr.ClientData()
                );

                worksheetDrawing.Append(anchor);
                drawingsPart.WorksheetDrawing = worksheetDrawing;
                drawingsPart.WorksheetDrawing.Save();
            }

            finalStream.Position = 0;
            return File(finalStream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"kitrequest-{id}.xlsx");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error generating Excel file: {ex.Message}");
        }
    }

    [HttpGet("KitRequests/Success/{id}")]
    public IActionResult Success(int id)
    {
        return View(model: id);
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