using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VexTeamNetwork.Models
{
    public class Division : Resources
    {
        [Key, ForeignKey("Competition"), Column(Order = 0)]
        public string Sku { get; set; }
        [Key, Column(Order = 1)]
        public string Name { get; set; }

        public virtual Competition Competition { get; set; }
    }
}
