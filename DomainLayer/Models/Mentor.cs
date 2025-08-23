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
        public string? JopTitle { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public List<string> Tags { get; set; } = new List<string>();
        public List<string> Skills { get; set; } = new List<string>();
    }
}
