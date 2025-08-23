using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public int MentorId { get; set; }
        public Mentor? Mentor { get; set; }

    }
}
