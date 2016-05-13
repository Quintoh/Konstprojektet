using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KonstProjektetV2.Helpers
{
    public static class ActiveNavbar
    {
        public static MvcHtmlString IsActive(this HtmlHelper activenavbar, string action, string controller, string activeClass, string inActiveClass = "")
        {
            var routeData = activenavbar.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && action == routeAction);

            return new MvcHtmlString(returnActive ? activeClass : inActiveClass);
        }
    }
}
