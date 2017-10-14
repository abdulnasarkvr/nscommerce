using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Web;

namespace NscBackOffice.Controllers
{
    public class FilesController : ApiController
    {
        [HttpPost]
        [ActionName("Upload")]

        public async Task<HttpResponseMessage> Upload()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var filename = postedFile.FileName;
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {

                            // YourModelProperty.imageurl = userInfo.email_id + extension;
                            //  where you want to attach your imageurl

                            //if needed write the code to update the table
                            var filePath = HttpContext.Current.Server.MapPath("/App_Data/Tmp/FileUploads/" + filename);
                            //  var filePath = HttpContext.Current.Server.MapPath("/App_Data/Tmp/FileUploads/" + extension);
                            //Userimage myfolder name where i want to save my image
                            postedFile.SaveAs(filePath);

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }

        //// This is from System.Web.Http, and not from System.Web.Mvc
        //public async Task<HttpResponseMessage> Upload1()
        //{
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    var provider = GetMultipartProvider();
        //    var result = await Request.Content.ReadAsMultipartAsync(provider);

        //    // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
        //    // so this is how you can get the original file name
        //    var originalFileName = GetDeserializedFileName(result.FileData.First());

        //    // uploadedFileInfo object will give you some additional stuff like file length,
        //    // creation time, directory name, a few filesystem methods etc..
        //    var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

        //    // Remove this line as well as GetFormData method if you're not
        //    // sending any form data with your upload request
        //    var fileUploadObj = GetFormData<UploadDataModel>(result);

        //    // Through the request response you can return an object to the Angular controller
        //    // You will be able to access this in the .success callback through its data attribute
        //    // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
        //    var returnData = "ReturnTest";
        //    return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        //}

        //// You could extract these two private methods to a separate utility class since
        //// they do not really belong to a controller class but that is up to you
        //private MultipartFormDataStreamProvider GetMultipartProvider()
        //{
        //    // IMPORTANT: replace "(tilde)" with the real tilde character
        //    // (our editor doesn't allow it, so I just wrote "(tilde)" instead)
        //    var uploadFolder = "/App_Data/Tmp/FileUploads"; // you could put this to web.config
        //    var root = System.Web.HttpContext.Current.Server.MapPath(uploadFolder);
        //    Directory.CreateDirectory(root);
        //    return new MultipartFormDataStreamProvider(root);
        //}

        //// Extracts Request FormatData as a strongly typed model
        //private object GetFormData<T>(MultipartFormDataStreamProvider result)
        //{
        //    if (result.FormData.HasKeys())
        //    {
        //        var unescapedFormData = Uri.UnescapeDataString(result.FormData
        //            .GetValues(0).FirstOrDefault() ?? String.Empty);
        //        if (!String.IsNullOrEmpty(unescapedFormData))
        //            return JsonConvert.DeserializeObject<T>(unescapedFormData);
        //    }

        //    return null;
        //}

        //private string GetDeserializedFileName(MultipartFileData fileData)
        //{
        //    var fileName = GetFileName(fileData);
        //    return JsonConvert.DeserializeObject(fileName).ToString();
        //}

        //public string GetFileName(MultipartFileData fileData)
        //{
        //    return fileData.Headers.ContentDisposition.FileName;
        //}
    }
}
