using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShinCacheTensei.Entities
{
    //Fire, Light, etc.
    public class Nature
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<DemonAffinity> DemonAffinities { get; set; }
    }
}