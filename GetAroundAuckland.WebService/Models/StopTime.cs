namespace GetAroundAuckland.WebService.Models
{
    public class StopTime
    {
        public string TripId { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string StopId { get; set; }
        public int StopSequence { get; set; }
        public string StopHeadsign { get; set; }
        public int? PickupType { get; set; }
        public int? DropOffType { get; set; }
        public int? ShapeDistance { get; set; }        
    }
}
