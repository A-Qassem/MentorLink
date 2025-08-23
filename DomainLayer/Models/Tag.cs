namespace DomainLayer.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int MentorId { get; set; }
        public Mentor? Mentor { get; set; }

    }
}
