using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DomainLayer.Models
{
    public class Phase
    {
        [Key]
        public string PhaseId { get; set; }
        public string? Title { get; set; }
        [AllowNull]
        public int Month { get; set; }
        [AllowNull]
        public int Weeks { get; set; }
        public List<string> Skills_Gained { get; set; } = new List<string>();

        public List<string> Recommended_Courses { get; set; } = new List<string>();

        public List<string> Prerequisites { get; set; } = new List<string>();
        public RoadMap? RoadMap { get; set; }
    }
}
