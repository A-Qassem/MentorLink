namespace DomainLayer.Models
{
    public class RecommendedCourse
    {
        public int Id { get; set; } 
        public string CourseName { get; set; } = null!;
        public Phase? Phase { get; set; }
    }
}
