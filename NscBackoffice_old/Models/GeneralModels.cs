using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NscBackoffice.Models
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
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

            string privilegeLevels = "Super";// string.Join("", GetUserRights(httpContext.User.Identity.Name.ToString())); // Call another method to get rights of the user from DB

            if (privilegeLevels.Contains(this.AccessLevel))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
