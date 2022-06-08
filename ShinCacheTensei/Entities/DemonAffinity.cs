using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShinCacheTensei.Entities
{
    ///Cada Demon pode ter fraquezas e forças de várias categorias. Um demon [Demon] pode absorver [AffinityType], por exemplo, Fire, Ice e Light [Nature].
    public class DemonAffinity
    {
        [JsonIgnore]
        public int DemonId { get; set; }

        [JsonIgnore]
        public int NatureId { get; set; }

        [JsonIgnore]
        public int AffinityTypeId { get; set; }

        [JsonIgnore]
        public Demon Demon  { get; set; }

        public Nature Nature { get; set; }

        public AffinityType AffinityType { get; set; } 
    }
}