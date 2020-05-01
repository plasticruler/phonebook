using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Fortex.Web.Mvc.Extensions
{
    public static class HtmlHelperExtensions
    {
        //public static IHtmlContent ActionLinkForDelete(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostname, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool showActionLinkAsDisabled)
        public static IHtmlContent ActionLinkForDelete(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {                        
            return htmlHelper.ActionLink(linkText,actionName,controllerName,null,null,null,routeValues, htmlAttributes);
        }
    }
}
