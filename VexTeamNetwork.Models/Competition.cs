using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VexTeamNetwork.Models
{
    public class Competition : Resources
    {
        public const string RegexMatcher = "^RE-(VEXU|VRC)-\\d{2}-\\d{4}$";

        [Key, MaxLength(16), RegularExpression(RegexMatcher)]
        public string Sku { get; set; }

        [Url]
        public string RobotEventsUrl { get; set; }

        public Program Program { get; set; }

        public string Name { get; set; }

        //[Column(TypeName = "datetime2")]
        public DateTime Start { get; set; }

        //[Column(TypeName = "datetime2")]
        public DateTime End { get; set; }

        public string Season { get; set; }

        public string Venue { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }

        public ICollection<Division> Divisions { get; set; }

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
