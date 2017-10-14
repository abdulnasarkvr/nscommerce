using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Net;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Web.Http.Controllers;
using NsCommerce;

namespace NscBackOffice
{
    public class Utility
    {
        public static void SaveException(Exception ex)
        {
            try
            {

                NsUtility.SaveException(ex, Utility.GetIPAddress(), "NscBackOffice", Utility.GetCurrentUser().ID);
            }            
            catch
            {

            }
        }

        public static User GetCurrentUser()
        {
            User _user = new User();
            try
            {

                _user.ID = 1;
            }
            catch (Exception ex)
            {
                NsUtility.SaveException(ex);
            }

            return _user;
        }        

        public static string GetIPAddress()
        {
            try
            {
                HttpContext context = HttpContext.Current;
                string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    string[] addresses = ipAddress.Split(',');
                    if (addresses.Length != 0)
                    {
                        return addresses[0];
                    }
                }

                return context.Request.ServerVariables["REMOTE_ADDR"];
            }
            catch(Exception ex)
            {
                NsUtility.SaveException(ex);
            }
            return "";
        }
    }

    public class User
    {
        public int ID { get; set; }
    }
    public class ApiReturn
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object ReturnData { get; set; }
    }

    public class CustomAuthorizeAttribute1 : AuthorizeAttribute
    {
        // Custom property
        public string AccessLevel { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //var isAuthorized = base.AuthorizeCore(httpContext);
            //if (!isAuthorized)
            //{
            //    return false;
            //}

            string privilegeLevels = "";
            if (HttpContext.Current.Session["Role"]!=null)
            {
                privilegeLevels = HttpContext.Current.Session["Role"].ToString();
            }
         // string.Join("", GetUserRights(httpContext.User.Identity.Name.ToString())); // Call another method to get rights of the user from DB

            if (privilegeLevels==this.AccessLevel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class CustomAuthorizeAttribute : System.Web.Http.Filters.AuthorizationFilterAttribute
    {
        public string AccessLevel { get; set; }
        public override bool AllowMultiple
        {
            get { return false; }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //Perform your logic here
            try
            {
                //actionContext.RequestContext.s
                string privilegeLevels = "";
                if (HttpContext.Current.Session["Role"] != null)
                {
                    privilegeLevels = HttpContext.Current.Session["Role"].ToString();
                }
                // string.Join("", GetUserRights(httpContext.User.Identity.Name.ToString())); // Call another method to get rights of the user from DB

                if (privilegeLevels == this.AccessLevel)
                {
                    base.OnAuthorization(actionContext);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                NsUtility.SaveException(ex);
            }

        }
    }


}