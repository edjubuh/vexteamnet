using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

namespace VexTeamNetwork.Models
{
    public class Match
    {
        public Match()
        {
            RedScoreDetails = new ExpandoObject();
            BlueScoreDetails = new ExpandoObject();
        }

        [Key, ForeignKey("Division"), Column(Order = 0), MaxLength(16)]
        public string Sku { get; set; }
        [Key, ForeignKey("Division"), Column(Order = 1)]
        public string DivisionName { get; set; }

        public virtual Division Division { get; set; }

        [Key, Column(Order = 2)]
        public Round Round { get; set; }

        [Key, Column(Order = 3)]
        public int Instance { get; set; }

        [Key, Column(Order = 4)]
        public int Number { get; set; }

        public DateTime Scheduled { get; set; }

        public string Field { get; set; }

        public int RedScore { get; set; }

        public int BlueScore { get; set; }

        public bool OfficialScore { get; set; }

        [ForeignKey("Red1"), MaxLength(6)]
        public string Red1Number { get; set; }
        public virtual Team Red1 { get; set; }

        [ForeignKey("Red2"), MaxLength(6)]
        public string Red2Number { get; set; }
        public virtual Team Red2 { get; set; }

        [ForeignKey("Red3"), MaxLength(6)]
        public string Red3Number { get; set; }
        public virtual Team Red3 { get; set; }

        [ForeignKey("RedSit"), MaxLength(6)]
        public string RedSitNumber { get; set; }
        public virtual Team RedSit { get; set; }

        [ForeignKey("Blue1"), MaxLength(6)]
        public string Blue1Number { get; set; }
        public virtual Team Blue1 { get; set; }

        [ForeignKey("Blue2"), MaxLength(6)]
        public string Blue2Number { get; set; }
        public virtual Team Blue2 { get; set; }

        [ForeignKey("Blue3"), MaxLength(6)]
        public string Blue3Number { get; set; }
        public virtual Team Blue3 { get; set; }

        [ForeignKey("BlueSit"), MaxLength(6)]
        public string BlueSitNumber { get; set; }
        public virtual Team BlueSit { get; set; }

        public dynamic RedScoreDetails { get; set; }

        public dynamic BlueScoreDetails { get; set; }
    }
}
