namespace DomainLayer.Models
{
    public class Prerequisite
    {
        public int Id { get; set; } 
        public string Requirement { get; set; } = null!;
        public ICollection<Phase> Phases { get; set; } = new List<Phase>();
    }
}
