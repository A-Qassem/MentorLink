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

        public required string Password { get; set; }
        public required string Phone { get; set; }

        public Level Level { get; set; } = Level.Beginner;

        public bool IsUpdated { get; set; } = false;

        public bool IsSubscribed { get; set; } = false;
        public string? sessionId { get; set; }
        public ICollection<Language> PreferredLanguages { get; set; } = new List<Language>();
        public ICollection<FocusArea> FocusAreas { get; set; } = new List<FocusArea>();
        public Rate? rate { get; set; }
        public RoadMap? RoadMap { get; set; }
        public int? MentorId { get; set; }
        public Mentor? Mentor { get; set; }
    }

}
