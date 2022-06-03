using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShinCacheTensei.Entities
{
    public class Demon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Race { get; set; }

        public int InitialLevel { get; set; }

        public int HP { get; set; }
        public int MP { get; set; }

        public int STR { get; set; }
        public int MAG { get; set; }
        public int VIT { get; set; }
        public int AGI { get; set; }
        public int LCK { get; set; }

        public RecruitingMethod RecruitingMethod { get; set; }
        public ICollection<DemonInitialSkill> DemonInitialSkills { get; set; }
        public ICollection<DemonAffinity> DemonAffinities { get; set; }

    }
}