using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Data.Models
{
    public class DemonIdListQueryParams
    {
        public int Resistance { get; set; }
        public int Weakness { get; set; }
        public int MinimumLevel { get; set; }
        public int MaximumLevel { get; set; }
        public int DemonRace { get; set; }
        public int Skill { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
    }
}