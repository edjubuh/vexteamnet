using System.Collections.Generic;
using Newtonsoft.Json;

namespace VexTeamNetwork.AutomatedDownloader.Models
{
    class RootObject<T>
    {
        [JsonConverter(typeof(BooleanConverter))]
        public bool status { get; set; }

        public int size { get; set; }

        public List<T> result { get; set; }

        public int error_code { get; set; }

        public string error_text { get; set; }
    }
}
