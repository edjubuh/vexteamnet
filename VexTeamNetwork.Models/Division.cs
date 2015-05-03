using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace VexTeamNetwork.Models
{
    [DataContract]
    public class Division : Resources
    {
        [Key, ForeignKey("Competition"), Column(Order = 0), DataMember]
        public string Sku { get; set; }
        [Key, Column(Order = 1), DataMember]
        public string Name { get; set; }

        [IgnoreDataMember]
        public virtual Competition Competition { get; set; }
    }
}
