using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace VexTeamNetwork.Models
{
    [DataContract]
    public class Competition : Resources
    {
        public const string RegexMatcher = "^RE-(VEXU|VRC)-\\d{2}-\\d{4}$";

        [Key, MaxLength(16), RegularExpression(RegexMatcher), DataMember]
        public string Sku { get; set; }

        [Url, DataMember]
        public string RobotEventsUrl { get; set; }

        [DataMember]
        public Program Program { get; set; }

        [DataMember]
        public string Name { get; set; }

        //[Column(TypeName = "datetime2")]
        [DataMember]
        public DateTime Start { get; set; }

        //[Column(TypeName = "datetime2")]
        [DataMember]
        public DateTime End { get; set; }

        [DataMember]
        public string Season { get; set; }

        [DataMember] public string Venue { get; set; }
        [DataMember] public string Address { get; set; }
        [DataMember] public string City { get; set; }
        [DataMember] public string Region { get; set; }
        [DataMember] public string Postcode { get; set; }
        [DataMember] public string Country { get; set; }

        [DataMember]
        public ICollection<Division> Divisions { get; set; }

        [IgnoreDataMember]
        public ICollection<Award> Awards { get; set; }

        [IgnoreDataMember]
        public ICollection<Award> QualifyingAwards { get; set; }

        public virtual string Time
        {
            get
            {
                if (Start.ToShortDateString().Equals(End.ToShortDateString()))
                    return Start.ToShortDateString();
                else
                    return Start.ToShortDateString() + " - " + End.ToShortDateString();
            }
        }

        [DataType(DataType.MultilineText)]
        public virtual string Location
        {
            get
            {
                string s = Address;
                if (!String.IsNullOrEmpty(City))
                    s += "\n" + City;
                if (!String.IsNullOrEmpty(Region))
                    s += ", " + Region;
                if (!String.IsNullOrEmpty(Postcode))
                    s += " " + Postcode;
                if (!String.IsNullOrEmpty(Country))
                    s += " " + Country;
                return s;
            }
        }
    }
}
