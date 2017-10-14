using System;
using System.Collections.Generic;
using System.Data;

namespace NsCommerce
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductCode { get; set; }                
        public string ProductName { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Details { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }        
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public int? UOMID { get; set; }
        public int? BrandID { get; set; }

        public CreateProductResponse Create(string IPAddress = null, int? CreatedUserID = null)
        {
            CreateProductResponse _response = new CreateProductResponse();
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                sqlHelper.AddSetParameterToSQLCommand("ProductCode", SqlDbType.VarChar, this.ProductCode);
                sqlHelper.AddSetParameterToSQLCommand("ProductName", SqlDbType.NVarChar, this.ProductName);
                sqlHelper.AddSetParameterToSQLCommand("ShortName", SqlDbType.VarChar, this.ShortName);
                sqlHelper.AddSetParameterToSQLCommand("LongName", SqlDbType.NVarChar, this.LongName);
                sqlHelper.AddSetParameterToSQLCommand("Details", SqlDbType.NVarChar, this.Details);
                sqlHelper.AddSetParameterToSQLCommand("LongDescription", SqlDbType.NVarChar, this.LongDescription);
                sqlHelper.AddSetParameterToSQLCommand("ShortDescription", SqlDbType.NVarChar, this.ShortDescription);
                
                sqlHelper.AddSetParameterToSQLCommand("CreatedUserID", SqlDbType.Int, CreatedUserID);
                sqlHelper.AddSetParameterToSQLCommand("UOMID", SqlDbType.Int, this.UOMID);
                sqlHelper.AddSetParameterToSQLCommand("BrandID", SqlDbType.Int, this.BrandID);
                sqlHelper.AddSetParameterToSQLCommand("IPAddress", SqlDbType.VarChar, IPAddress);

                DataSet ds = sqlHelper.GetDatasetByCommand("Product_Create");

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["DBSTATUS"].ToString() == "SUCCESS")
                            {
                                if (ds.Tables.Count > 1)
                                {
                                    if (ds.Tables[1].Rows.Count > 0)
                                    {
                                        _response.Status = ActivityStatus.SUCCESS;
                                        _response.ID = Convert.ToInt32(ds.Tables[1].Rows[0]["ID"].ToString());
                                    }
                                }

                            }
                            else if (ds.Tables[0].Rows[0]["DBSTATUS"].ToString() == "DUPLICATE")
                            {
                                _response.Status = ActivityStatus.DUPLICATE;
                            }
                            else
                            {
                                _response.Status = ActivityStatus.ERROR;
                            }
                        }
                        else
                        {
                            _response.Status = ActivityStatus.ERROR;
                        }
                    }
                    else
                    {
                        _response.Status = ActivityStatus.ERROR;
                    }
                }
                else
                {
                    _response.Status = ActivityStatus.ERROR;
                }
            }
            catch (Exception ex)
            {
                _response.Status = ActivityStatus.EXCEPTION;
                NsUtility.SaveException(ex, "", "NsCommerce");
            }

            return _response;
        }


    }

    public class CreateProductResponse
    {
        public ActivityStatus Status { get; set; }
        public string Message { get; set; }
        public int ID { get; set; }
    }

    public class GetProductListResponse
    {
        public ActivityStatus Status { get; set; }
        public string Message { get; set; }
        public List<Product> ProductList { get; set; }
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
    }
}
