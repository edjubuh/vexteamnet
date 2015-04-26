using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VexTeamNetwork.AutomatedDownloader.Models
{
    class EnumDisplayNameConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader.Value.ToString();
            if (String.IsNullOrEmpty(value))
                return Enum.GetValues(objectType).GetValue(0);
            foreach(Enum val in Enum.GetValues(objectType))
            {
                FieldInfo fi = objectType.GetField(val.ToString());
                DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attributes[0].GetDescription() == value || attributes[0].GetName() == value)
                    return val;
            }
            throw new ArgumentException("The value '" + value + "' is not supported.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
