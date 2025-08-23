using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public enum Level
    {
        Beginner,
        Intermediate,
        Advanced
    }
    public class Trainee
    {
        public int TraineeId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        public string? PictureUrl { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string Phone { get; set; }

        public Level Level { get; set; } = Level.Beginner;

        public bool IsUpdated { get; set; } = false;

        public bool IsSubscribed { get; set; } = false;
        public string? SessionId { get; set; }
        public ICollection<Language> PreferredLanguages { get; set; } = new List<Language>();
        public ICollection<FocusArea> FocusAreas { get; set; } = new List<FocusArea>();
        public ICollection<Rate> Rates { get; set; } = new List<Rate>();

        public RoadMap? RoadMap { get; set; }
        public int? MentorId { get; set; }
        public Mentor? Mentor { get; set; }
    }

}
