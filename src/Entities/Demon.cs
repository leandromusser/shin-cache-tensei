using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShinCacheTensei.Entities
{
    public class Demon
    {

        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public int DemonRaceId { get; set; }
        
        public DemonRace Race { get; set; }

        public int? InitialLevel { get; set; }

        //Transformar esses stats em um objeto de valor?
        public int? InitialHp { get; set; }
        public int? InitialMp { get; set; }
        public int? InitialStr { get; set; }
        public int? InitialMag { get; set; }
        public int? InitialVit { get; set; }
        public int? InitialAgi { get; set; }
        public int? InitialLck { get; set; }

        [JsonIgnore]
        public int RecruitingMethodId { get; set; }
        public RecruitingMethod RecruitingMethod { get; set; }
        public ICollection<DemonInitialSkill> DemonInitialSkills { get; set; }
        public ICollection<DemonAffinity> DemonAffinities { get; set; }

    }
}