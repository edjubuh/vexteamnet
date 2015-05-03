using System;
using System.Runtime.Serialization;

namespace VexTeamNetwork.Models
{
    [DataContract]
    public class Resources
    {
        [DataMember]
        public string LastModifierUserId { get; set; }

        [DataMember]
        public DateTime LastModifiedTime { get; set; }
    }
}
