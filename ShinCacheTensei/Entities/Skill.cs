using System.Text.Json.Serialization;

namespace ShinCacheTensei.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public int SkillTypeId { get; set; }
        public SkillType SkillType { get; set; }
        public int Cost { get; set; }
    }
}