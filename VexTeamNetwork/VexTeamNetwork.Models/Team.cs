using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VexTeamNetwork.Models
{
    public class Team : Resources
    {
        public const string RegexMatcher = "^[1-9]\\d{0,3}[A-Z]{0,1}$|^[A-Z]{0,4}[1-9]{0,2}$";

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), StringLength(6, MinimumLength=1)]
        [RegularExpression(RegexMatcher, ErrorMessage = "Must be a valid team number.")]
        public string Number { get; set; }

        public string TeamName { get; set; }

        [Display(Name = "Robot Name")]
        public string RobotName { get; set; }

        public string Organization { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public string Location
        {
            get
            {
                string s = City;
                if (!String.IsNullOrEmpty(Region))
                    s += ", " + Region;
                if (!String.IsNullOrEmpty(Country) && !Country.Equals("United States", StringComparison.CurrentCultureIgnoreCase))
                    s += ", " + Country;
                return s;
            }
        }

        public bool IsRegistered { get; set; }

        public Grade? Grade { get; set; }

        public Program? Program { get; set; }
    }
}
