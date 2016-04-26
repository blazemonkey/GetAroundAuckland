namespace GetAroundAuckland.WebService.Models
{
    public class Shape
    {
        public string Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Sequence { get; set; }
        public int? Distance { get; set; }
    }
}
