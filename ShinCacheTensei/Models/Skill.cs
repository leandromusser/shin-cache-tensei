namespace ShinCacheTensei.Models
{
    public class Skill: CommonEntity
    {
        public string Description { get; set; }
        public SkillCost Cost { get; set; }
        public SkillCategory Category { get; set; }
    }
}