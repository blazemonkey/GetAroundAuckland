namespace GetAroundAuckland.WebService.Models
{
    public class Stop
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ZoneId { get; set; }
        public int Code { get; set; }
        public byte LocationType { get; set; }
        public string ParentStation { get; set; }
    }
}
