using System;
using System.Web.Http;
using NsCommerce;
using System.Collections.Generic;

namespace NscService.Controllers
{
   // [ApiAuthorize]
    public class ProductController : ApiController
    {
        [HttpGet]
        [ActionName("GetList")]
        public NscReturn GetList(int pageNo,int pageSize, string searchKey = null, int? CategoryID=null)
        {
            
            NscReturn _return = new NscReturn();
            try
            {
                //int a = 0;
                //int b = 1 / a;
                DataService _dataService = new DataService();

                GetProductListResponse _getProductListResponse = _dataService.GetProductList(pageNo, pageSize, searchKey, CategoryID);

                _return.status = ActivityStatus.SUCCESS;

                if (_getProductListResponse.Status == ActivityStatus.SUCCESS)
                {
                    _return.returnData = new
                    {

                        ProductList = _getProductListResponse.ProductList,
                        TotalPages = _getProductListResponse.TotalPages,
                        TotalRows = _getProductListResponse.TotalRows
                    };

                    if (_getProductListResponse.ProductList.Count > 0)
                    {                       
                        _return.message = "Product list selected";                        
                    }
                    else
                    {                        
                        _return.message = "No records selected";
                    }
                }
                else
                {
                    _return.message = "No records selected";
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
    }
}
