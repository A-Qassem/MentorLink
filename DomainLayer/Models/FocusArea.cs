namespace DomainLayer.Models
{
    public class FocusArea
    {
        public int FocusAreaId { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
    }

}
