﻿using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using VexTeamNetwork.Models;

namespace VexTeamNetwork.AutomatedDownloader.Models
{
    class Team : Resources
    {
        static Func<DisplayAttribute, string> enumLambda = (t) => t.GetDescription();

        [Key, MaxLength(6)]
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        [JsonProperty("robot_name")]
        public string RobotName { get; set; }

        [JsonProperty("organisation")]
        public string Organization { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("program"), JsonConverter(typeof(EnumDisplayNameConverter))]
        public VexTeamNetwork.Models.Program Program { get; set; }

        [JsonProperty("grade"), JsonConverter(typeof(EnumDisplayNameConverter))]
        public Grade Grade { get; set; }

        [JsonProperty("is_registered"), JsonConverter(typeof(BooleanConverter))]
        public bool IsRegistered { get; set; }

        public bool Equals(Team other)
        {
            return
                this.Number == other.Number &&
                this.TeamName == other.TeamName &&
                this.RobotName == other.RobotName &&
                this.Organization == other.Organization &&
                this.City == other.City &&
                this.Country == other.Country &&
                this.Program == other.Program &&
                this.Grade == other.Grade &&
                this.IsRegistered == other.IsRegistered;
        }

        public bool AnyEquivalent(Team other)
        {
            return
                this.Number == other.Number ||
                this.TeamName == other.TeamName ||
                this.RobotName == other.RobotName ||
                this.Organization == other.Organization ||
                this.City == other.City ||
                this.Country == other.Country ||
                this.Program == other.Program ||
                this.Grade == other.Grade ||
                this.IsRegistered == other.IsRegistered;
        }
    }
}
