
using NscBackOffice.Models;
using System;
using System.Data;
using System.Linq;
using System.Web.Http;
using NsCommerce;

namespace NscBackOffice.Controllers
{
    //[CustomAuthorize(AccessLevel = "Admin")]   
    public class ProductController : ApiController
    {
        [HttpPost]
        [ActionName("Create")]
        public NscReturn Create([FromBody]CreateProductViewModel model)
        {
            NscReturn _return = new NscReturn();
            try
            {
                if (ModelState.IsValid)
                {
                    NsCommerce.Product _product = new NsCommerce.Product();                    

                    _product.ProductCode=model.ProductCode;
                    _product.ProductName=model.ProductName;
                    _product.ShortName=model.ShortName;
                    _product.LongName=model.LongName;
                    _product.Details=model.Details;
                    _product.LongDescription=model.LongDescription;
                    _product.ShortDescription=model.ShortDescription;
                    _product.UOMID=model.UOMID;
                    _product.BrandID=model.BrandID;

                    CreateProductResponse _createProductResponse =
                        _product.Create( Utility.GetIPAddress(), Utility.GetCurrentUser().ID);

                    _return.status = _createProductResponse.Status;

                    if (_createProductResponse.Status == ActivityStatus.SUCCESS)
                    {
                        _return.returnData = new {
                            id = _createProductResponse.ID
                        };
                    }
                   
                    
                }
                else
                {
                    _return.status = ActivityStatus.INVALID;
                    _return.message = "Invalid data";
                    var validationMessage = new
                    {
                        validationErrors = ModelState
                         .SelectMany(ms => ms.Value.Errors)
                         .Select(ms => ms.ErrorMessage)
                    };
                    _return.returnData = validationMessage;
                }

            }
            catch (Exception ex)
            {
                _return.status = ActivityStatus.EXCEPTION;
                _return.message = "Something went wrong";

                Utility.SaveException(ex);
            }
            return _return;
        }               

        [HttpGet]
        [ActionName("SelectList")]
        public NscReturn SelectList()
        {
            NscReturn _return = new NscReturn();

            return _return;
        }

    }
       
}
