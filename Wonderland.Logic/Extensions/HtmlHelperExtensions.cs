
namespace Wonderland.Logic.Extensions
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Linq.Expressions;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// http://coding.abel.nu/2013/06/drying-mvc-forms-with-an-editorentryfor-helper/
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString EditorForField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            return BuildEditorForField(htmlHelper.LabelFor(expression), htmlHelper.EditorFor(expression), htmlHelper.ValidationMessageFor(expression));
        }

        private static MvcHtmlString BuildEditorForField(MvcHtmlString label, MvcHtmlString input, MvcHtmlString validation)
        {
            return new MvcHtmlString("<div class=\"editor-label\">" + label + "</div>\n<div class=\"editor-field\">" + input + validation + "</div>\n\n");
        }

        //public static string GetAntiForgeryToken(this HtmlHelper htmlHelper)
        //{
        //    string oldCookieToken = null, newCookieToken, formToken;

        //    var oldCookie = htmlHelper.ViewContext.HttpContext.Request.Cookies[AntiForgeryConfig.CookieName];
        //    if (oldCookie != null)
        //    {
        //        oldCookieToken = oldCookie.Value;
        //    }

        //    AntiForgery.GetTokens(oldCookieToken, out newCookieToken, out formToken);
        //    return formToken;
        //}
    }
}
