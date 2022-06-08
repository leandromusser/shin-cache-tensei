using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShinCacheTensei.Entities
{
    public class DemonInitialSkill
    {
        [JsonIgnore]
        public int DemonId { get; set; }
        [JsonIgnore]
        public int SkillId { get; set; }
        [JsonIgnore]
        public Demon Demon { get; set; }
        public int UnlockLevel { get; set; }
        public Skill Skill { get; set; }
    }
}
