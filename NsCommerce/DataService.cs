using System;
using System.Collections.Generic;
using System.Data;

namespace NsCommerce
{
    public class DataService
    {
        public GetProductListResponse GetProductList(int pageNo, int pageSize, string searchKey = null, int? CategoryID = null)
        {
            GetProductListResponse _getProductListResponse = new GetProductListResponse();
            _getProductListResponse.ProductList = new List<Product>();
            try
            {

                SqlHelper sqlHelper = new SqlHelper();

                sqlHelper.AddSetParameterToSQLCommand("searchKey", SqlDbType.VarChar, searchKey);
                sqlHelper.AddSetParameterToSQLCommand("pageNo", SqlDbType.Int, pageNo);
                sqlHelper.AddSetParameterToSQLCommand("pageSize", SqlDbType.Int, pageSize);
                sqlHelper.AddSetParameterToSQLCommand("CategoryID", SqlDbType.Int, CategoryID);

                DataSet ds = sqlHelper.GetDatasetByCommand("Product_List");

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["DBSTATUS"].ToString() == "SUCCESS")
                            {
                                _getProductListResponse.TotalPages = Convert.ToInt32(
                                    ds.Tables[0].Rows[0]["TotalPages"].ToString());
                                _getProductListResponse.TotalRows = Convert.ToInt32(
                                   ds.Tables[0].Rows[0]["TotalRows"].ToString());

                                if (ds.Tables.Count > 1)
                                {
                                    _getProductListResponse.Status = ActivityStatus.SUCCESS;
                                    foreach (DataRow dr in ds.Tables[1].Rows)
                                    {
                                        _getProductListResponse.ProductList.Add(new Product
                                        {
                                            ID= Convert.ToInt32(dr["ID"].ToString()),
                                            ProductCode=dr["ProductCode"].ToString(),
                                            ProductName=dr["ProductName"].ToString(),
                                            ShortName=dr["ShortName"].ToString (),
                                            LongName = dr["LongName"].ToString(),
                                            Details = dr["Details"].ToString(),
                                            LongDescription = dr["LongDescription"].ToString(),
                                            ShortDescription = dr["ShortDescription"].ToString(),
                                            
                                            Image1 = dr["Image1"].ToString(),
                                            Image2 = dr["Image2"].ToString(),
                                            Image3 = dr["Image3"].ToString(),
                                            UOMID = Convert.ToInt32(dr["UOMID"].ToString()),
                                            BrandID = Convert.ToInt32(dr["BrandID"].ToString())
                                        });
                                    }                                   
                                }
                                else
                                {
                                    _getProductListResponse.Status = ActivityStatus.NOROWS;
                                }
                            }
                            else
                            {
                                _getProductListResponse.Status = ActivityStatus.NOROWS;
                            }
                        }
                        else
                        {
                            _getProductListResponse.Status = ActivityStatus.ERROR;
                        }
                    }
                    else
                    {
                        _getProductListResponse.Status = ActivityStatus.ERROR;
                    }
                }
                else
                {
                    _getProductListResponse.Status = ActivityStatus.ERROR;
                }
            }
            catch (Exception ex)
            {                
                NsUtility.SaveException(ex, "", "NsCommerce");
            }
            return _getProductListResponse;
        }
    }
}
