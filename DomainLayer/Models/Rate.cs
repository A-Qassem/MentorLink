using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Rate
    {
        public int TraineeId { get; set; }
        public int MentorId { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; } 
        public List<Trainee> trainees = new List<Trainee>();
        public List<Mentor> mentors = new List<Mentor>();
    }
}
