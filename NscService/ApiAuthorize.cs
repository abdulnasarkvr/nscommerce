using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Http;

namespace NscService
{
    public class ApiAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (Authorize(actionContext))
            {
                return;
            }
            HandleUnauthorizedRequest(actionContext);
        }

        private bool Authorize(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            bool returnValue = false;
            try
            {
                if (ConfigurationManager.AppSettings["EnableTokenValidation"] != null)
                {
                    if (ConfigurationManager.AppSettings["EnableTokenValidation"].ToString() == "true")
                    {
                        var TokenValue = HttpContext.Current.Request.Headers["TokenValue"];
                        var ClientStrID = HttpContext.Current.Request.Headers["ClientStrID"];
                        if (TokenValue != null)
                        {

                            SqlHelper sqlHelper = new SqlHelper();

                            sqlHelper.AddSetParameterToSQLCommand("ClientStrID ", SqlDbType.NVarChar, ClientStrID);
                            sqlHelper.AddSetParameterToSQLCommand("TokenValue", SqlDbType.NVarChar, TokenValue);
                            DataSet ds = sqlHelper.GetDatasetByCommand("ValidateToken");

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {

                                    if (ds.Tables[0].Rows[0][0].ToString().ToUpper() == "SUCCESS")
                                    {
                                        returnValue = true;
                                    }

                                }

                            }


                        }
                    }
                    else
                    {
                        returnValue = true;
                    }
                }
                else
                {
                    returnValue = true;
                }
            }
            catch (Exception ex)
            {
                Utility.SaveException(ex);

            }
            return returnValue;

        }
    }
}
