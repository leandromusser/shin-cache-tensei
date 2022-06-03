using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShinCacheTensei.Entities
{
    public class DemonInitialSkill
    {
        public int DemonId { get; set; }
        public int SkillId { get; set; }
        public Demon Demon { get; set; }
        public int UnlockLevel { get; set; }
        public Skill Skill { get; set; }
    }
}
