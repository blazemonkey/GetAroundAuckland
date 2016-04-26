namespace GetAroundAuckland.WebService.Models
{
    public class Route
    {
        public string Id { get; set; }
        public string AgencyId { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public byte Type { get; set; }
        public string Color { get; set; }
        public string TextColor { get; set; }
    }
}
