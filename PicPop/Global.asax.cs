using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using PicPop.BusinessLogic;
using PicPop.BusinessLogic.Helpers;
using PicPop.Helper;

namespace PicPop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new SharedDataActionFilter(), 0);
        }

        private void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            try
            {
                string host = HttpContext.Current.Request.Url.Host;
               
                if (!host.Contains("localhost"))
                {
                    Exception exception = Server.GetLastError();

                    ErrorsLog log = new ErrorsLog
                    {
                        UserName = HttpContext.Current.User.Identity.Name,
                        UserAgent = Request.UserAgent,
                        Date = DateTime.Now,
                        Message = exception.GetBaseException().Message,
                        Method = exception.GetBaseException().TargetSite.ToString(),
                        Source = exception.GetBaseException().Source,
                        StackTrace = exception.GetBaseException().StackTrace,
                        Url =""// HttpContext.Current.Request.Url
                    };

                    ErrorLogHelper.Add(log);
                }
            }
            catch (Exception ex){ }
        }
    }
}
