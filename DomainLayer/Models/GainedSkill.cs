namespace DomainLayer.Models
{
    public class GainedSkill
    {
        public int Id { get; set; } 
        public string SkillName { get; set; } = null!;
        public ICollection<Phase> Phase { get; set; } = new List<Phase>();
    }
}
