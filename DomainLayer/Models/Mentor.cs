using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Mentor
    {
        public int MentorId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        public string? PictureUrl { get; set; }
        public required string Password { get; set; }
        public required string Phone { get; set; }
        public string? Description { get; set; }
        public string? JobTitle { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public ICollection<Rate> Rates { get; set; } = new List<Rate>();
        public List<Trainee> Trainees { get; set; } = new List<Trainee>();

    }
}
