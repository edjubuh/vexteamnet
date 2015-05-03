using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Runtime.Serialization;

namespace VexTeamNetwork.Models
{
    [DataContract]
    public class Match
    {
        public Match()
        {
            RedScoreDetails = new ExpandoObject();
            BlueScoreDetails = new ExpandoObject();
        }

        [Key, ForeignKey("Division"), Column(Order = 0), MaxLength(16), DataMember]
        public string Sku { get; set; }
        [Key, ForeignKey("Division"), Column(Order = 1), DataMember]
        public string DivisionName { get; set; }

        public virtual Division Division { get; set; }

        [Key, Column(Order = 2), DataMember, EnumDataType(typeof(Round))]
        public Round Round { get; set; }

        [Key, Column(Order = 3), DataMember]
        public int Instance { get; set; }

        [Key, Column(Order = 4), DataMember]
        public int Number { get; set; }
        
        [DataMember]
        public DateTime Scheduled { get; set; }

        [DataMember]
        public string Field { get; set; }

        [DataMember]
        public int RedScore { get; set; }

        [DataMember]
        public int BlueScore { get; set; }

        [DataMember]
        public bool OfficialScore { get; set; }

        [ForeignKey("Red1"), MaxLength(6), DataMember]
        public string Red1Number { get; set; }
        public virtual Team Red1 { get; set; }

        [ForeignKey("Red2"), MaxLength(6), DataMember]
        public string Red2Number { get; set; }
        public virtual Team Red2 { get; set; }

        [ForeignKey("Red3"), MaxLength(6), DataMember]
        public string Red3Number { get; set; }
        public virtual Team Red3 { get; set; }

        [ForeignKey("RedSit"), MaxLength(6), DataMember]
        public string RedSitNumber { get; set; }
        public virtual Team RedSit { get; set; }

        [ForeignKey("Blue1"), MaxLength(6), DataMember]
        public string Blue1Number { get; set; }
        public virtual Team Blue1 { get; set; }

        [ForeignKey("Blue2"), MaxLength(6), DataMember]
        public string Blue2Number { get; set; }
        public virtual Team Blue2 { get; set; }

        [ForeignKey("Blue3"), MaxLength(6), DataMember]
        public string Blue3Number { get; set; }
        public virtual Team Blue3 { get; set; }

        [ForeignKey("BlueSit"), MaxLength(6), DataMember]
        public string BlueSitNumber { get; set; }
        public virtual Team BlueSit { get; set; }

        [DataMember]
        public dynamic RedScoreDetails { get; set; }

        [DataMember]
        public dynamic BlueScoreDetails { get; set; }
    }
}
