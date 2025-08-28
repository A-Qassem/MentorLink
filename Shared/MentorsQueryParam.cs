using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class MentorsQueryParam
    {
        public int? TagId { get; set; }
        public int? SkillId { get; set; }
        public MentorSortingOptions SortingOptions { get; set; }
        public string? SearchValue { get; set; }
    }
}
