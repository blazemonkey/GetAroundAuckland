namespace GetAroundAuckland.WebService.Models
{
    public class Trip
    {
        public string Id { get; set; }
        public string RouteId { get; set; }
        public string ServiceId { get; set; }
        public string Headsign { get; set; }
        public int DirectionId { get; set; }
        public string BlockId { get; set; }
        public string ShapeId { get; set; }
        public string FirstArrivalTime { get; set; }
        public string LastDepartureTime { get; set; }
    }
}
