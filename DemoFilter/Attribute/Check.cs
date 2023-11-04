using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace DemoFilter.Attribute
{
    public class Check : ActionFilterAttribute, IAuthenticationFilter
    {

        object userName = null;
        void IAuthenticationFilter.OnAuthentication(AuthenticationContext filterContext)
        {
            userName = filterContext.HttpContext.Session["user"];
        }

        void IAuthenticationFilter.OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
           if (userName == null || (!userName.ToString().Equals(filterContext.HttpContext.Session["user"].ToString())))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}