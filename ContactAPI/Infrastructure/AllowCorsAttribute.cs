using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactAPI.Infrastructure
{
    public class AllowCORSAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.UrlReferrer != null)
            {
                filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
                filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers", "x-requested-with");
                filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}