namespace ShinCacheTensei.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillCost Cost { get; set; }
        public SkillCategory Category { get; set; }
    }
}