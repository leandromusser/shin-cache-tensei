using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShinCacheTensei.Entities
{
    public class SkillType
    {
        public int Id { get; set; }
        public string Type { get; set; } //HP, MP, PASSIVE

        [JsonIgnore]
        public ICollection<Skill> Skills { get; set; }
    }
}
