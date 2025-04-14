namespace Glenavon.JFC.WebApp.Models
{
    public class KitRequestModel
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string Status { get; set; }
        public List<KitItemModel> Players { get; set; }
    }

    public class KitItemModel
    {
        public string TopSize { get; set; }
        public string ShortsSize { get; set; }
        public string SocksSize { get; set; }
        public int ShirtNumber { get; set; }
        public string KitType { get; set; }
    }

}
