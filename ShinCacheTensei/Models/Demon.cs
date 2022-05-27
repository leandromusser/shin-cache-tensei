using System;
using System.Collections.Generic;

namespace ShinCacheTensei.Models
{
    public class Demon: CommonEntity
    {
        public string Race { get; set; }

        public int Level { get; set; }

        public RecruitingMethod RecruitingMethod { get; set; }
        public List<Skill> Skills { get; set; }
    }
}