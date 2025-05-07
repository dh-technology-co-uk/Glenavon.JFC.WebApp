namespace Glenavon.JFC.WebApp.Models;

public class EquipmentKitRequestModel
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string TeamName { get; set; }
    public string Status { get; set; }
    public List<KitItemModel> Players { get; set; }
    public string ManagerName { get; set; }
    public string ManagerMobile { get; set; }
    public string ManagerEmail { get; set; }
    public string AdditionalInfo { get; set; }
    public byte[] SponsorLogo { get; set; } // You can store the uploaded logo as byte[]
}

public class KitItemModel
{
    public string TopSize { get; set; }
    public string ShortsSize { get; set; }
    public string SocksSize { get; set; }
    public int ShirtNumber { get; set; }
    public string KitType { get; set; }
    public string QuarterZip { get; set; }
}