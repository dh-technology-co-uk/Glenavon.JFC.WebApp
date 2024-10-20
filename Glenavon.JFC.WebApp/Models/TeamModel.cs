namespace Glenavon.JFC.WebApp.Models
{
    public class TeamModel
    {
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public List<string> Coaches { get; set; }
        public string League { get; set; }
    }
}
