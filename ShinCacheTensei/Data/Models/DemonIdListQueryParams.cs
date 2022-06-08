using ShinCacheTensei.Entities;
using System.ComponentModel.DataAnnotations;

namespace ShinCacheTensei.Data.Models
{
    public class DemonIdListQueryParams
    {
        public int? ResistNatureId { get; set; }
        public int? WeakNatureId { get; set; }
        public int? MinimumLevel { get; set; }
        public int? MaximumLevel { get; set; }
        public int? DemonRaceId { get; set; }
        public int? SkillId { get; set; }
        public string ContainsThisTextInName { get; set; }

        public int? AfterId { get; set; }

        public override int GetHashCode()
        {
            return new { ResistNatureId, WeakNatureId, MinimumLevel, MaximumLevel, DemonRaceId, SkillId, ContainsThisTextInName, AfterId }.GetHashCode();
        }

    }
}