using System.Collections.Generic;

namespace ShinCacheTensei.Entities
{
    public class Demon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Race { get; set; }

        public int Level { get; set; }

        public RecruitingMethod RecruitingMethod { get; set; }

        public List<DemonSkill> DemonSkills { get; set; }
    }
}