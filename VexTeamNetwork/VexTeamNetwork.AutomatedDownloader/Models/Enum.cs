using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VexTeamNetwork.Models
{
    public static class EnumExtensions
    {
        public static string GetDisplayAttribute(this Enum val, Func<DisplayAttribute, string> lambda)
        {
            string enumName = val.ToString();
            DisplayAttribute[] attributes =
                (DisplayAttribute[])val.GetType()
                .GetField(enumName)
                .GetCustomAttributes(typeof(DisplayAttribute), false);


            return attributes.Length > 0 ? lambda(attributes[0]) : enumName;
        }
    }
}