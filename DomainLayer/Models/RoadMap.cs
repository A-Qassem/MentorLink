using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class RoadMap
    {
        public int RoadMapId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Total_Months { get; set; }
        public int Total_Hours { get; set; }
        public Level Diffculty_Level { get; set; }  
        public List<Phase> Phases { get; set; } = new List<Phase>();
        public List<string> Career_Outcomes { get; set; } = new List<string>();
    }
}
