namespace ShinCacheTensei.Entities
{
    ///Cada Demon pode ter fraquezas e forças de várias categorias. Um demon [Demon] pode absorver [AffinityType], por exemplo, Fire, Ice e Light [Nature].
    public class DemonAffinity
    {
        public Demon Demon  { get; set; }
        public Nature Nature { get; set; }
        public AffinityType AffinityType { get; set; } 
    }
}