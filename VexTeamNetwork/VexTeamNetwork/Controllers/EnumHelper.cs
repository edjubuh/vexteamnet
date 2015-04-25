using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;


namespace VexTeamNetwork.Models
{
    public static class EnumHelper
    {
        public static IHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return DescriptionFor<TModel, TValue>(helper, expression, null);
        }

        public static IHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string descriptionText)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string text = descriptionText ?? metadata.Description;

            if (string.IsNullOrEmpty(text)) return new HtmlString("");

            TagBuilder tag = new TagBuilder("span");
            tag.SetInnerText(text);

            return new HtmlString(tag.ToString(TagRenderMode.Normal));
        }

        public static string GetDisplayAttribute(this Enum val, Func<DisplayAttribute, string> lambda)
        {
            string enumName = val.ToString();
            DisplayAttribute[] attributes =
                (DisplayAttribute[])val.GetType()
                .GetField(enumName)
                .GetCustomAttributes(typeof(DisplayAttribute), false);


            return attributes.Length > 0 ? lambda(attributes[0]) : enumName;
        }

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = e.ToString() };

            return new SelectList(values, "Id", "Name", enumObj);
        }
    }
}
