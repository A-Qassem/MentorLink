namespace DomainLayer.Models
{
    public class CareerOutcome
    {
        public int Id { get; set; }
        public string Outcome { get; set; } = null!;
        public int RoadMapId { get; set; }
        public RoadMap? RoadMap { get; set; }
    }
}
