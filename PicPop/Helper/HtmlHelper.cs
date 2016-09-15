using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PicPop
{
    public static class HtmlHelper
    {
        /// <summary>
        /// http://www.jlum.ws/post/2013/11/5/displaying-enum-values-as-radio-button-options
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonsForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var listOfValues = Enum.GetNames(metaData.ModelType);
            var sb = new StringBuilder();

            if (listOfValues != null)
            {
                sb = sb.AppendFormat("<ul>");
                // Create a radio button for each item in the list
                foreach (var name in listOfValues)
                {
                    var label = name;
                    var memInfo = metaData.ModelType.GetMember(name);

                    if (memInfo != null)
                    {
                        var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                        if (attributes != null && attributes.Length > 0)
                            label = ((DisplayAttribute)attributes[0]).Name;
                    }

                    var id = string.Format(
                        "{0}_{1}_{2}",
                        htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix,
                        metaData.PropertyName,
                        name
                    );

                    var radio = htmlHelper.RadioButtonFor(expression, name, new { id = id }).ToHtmlString();
                    sb.AppendFormat("<li>{0}{1}</li>", radio, HttpUtility.HtmlEncode(label));
                }
                sb = sb.AppendFormat("</ul>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}