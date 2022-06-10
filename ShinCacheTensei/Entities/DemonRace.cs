using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShinCacheTensei.Entities
{
    public class DemonRace
    {
        [JsonIgnore]
        public ICollection<Demon> Demons { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}