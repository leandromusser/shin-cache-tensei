using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShinCacheTensei.Entities
{
    ///Cada Demon pode ter fraquezas e forças de várias categorias. Um demon [Demon] pode absorver [AffinityType], por exemplo, Fire, Ice e Light [Nature].
    public class DemonAffinity
    {
        public int DemonId { get; set; }

        public int NatureId { get; set; }

        public int AffinityTypeId { get; set; }

        public Demon Demon  { get; set; }

        public Nature Nature { get; set; }

        public AffinityType AffinityType { get; set; } 
    }
}