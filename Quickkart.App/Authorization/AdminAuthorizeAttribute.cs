using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quickkart.App.Authorization
{
    public class AdminAuthorizeAttribute: AuthorizeAttribute
    {
        private string notifyUrl = "/Home/MyHome";
        public string NotifyUrl {
            get { return notifyUrl; }
            set { notifyUrl = value; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool status = false;
            try
            {
                var userName = HttpContext.Current.Session["userName"];
                string role = HttpContext.Current.Session["role"].ToString();
                if(userName == null)
                {
                    status = false;
                }
                else
                {
                    if(role == "1" || role == "2")
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
            }
            catch(Exception e)
            {
                status = false;
            }
            return status;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result=new RedirectResult(NotifyUrl);
        }
    }
}