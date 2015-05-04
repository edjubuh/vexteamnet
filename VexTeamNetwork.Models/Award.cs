using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VexTeamNetwork.Models
{
    [DataContract]
    public class Award : Resources
    {
        [Key, Column(Order = 1), ForeignKey("Competition"), MaxLength(16), DataMember]
        public string CompetitionSku { get; set; }

        [Key, Column(Order = 2), DataMember]
        public string Name { get; set; }

        [Key, Column(Order = 3), ForeignKey("Team"), MaxLength(6), DataMember]
        public string TeamNumber { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual Team Team { get; set; }
        
        public virtual ICollection<Competition> QualifyingCompetitions { get; set; }
    }
}
