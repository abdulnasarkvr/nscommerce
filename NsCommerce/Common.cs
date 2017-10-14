using System;
using System.Data;
using System.IO;
using System.Text;

namespace NsCommerce
{
    public class NsUtility
    {
        public static void SaveException
            (Exception ex,string IPAddress = null,string ModuleName=null,int? UserID=null)
        {
            try
            {
                string path = AppContext.BaseDirectory + @"\Logs";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string FileName = AppContext.BaseDirectory + @"\Logs\" +ModuleName + "_" + DateTime.Now.ToString("yyyMMdd-hh") + ".txt";
                if (!CheckIfLogFileExists(FileName))//log file not exists
                {
                    CreateLogFile(FileName); //create log file
                }
                if (CheckIfLogFileExists(FileName))
                {
                    LogException(FileName, ex, IPAddress, UserID);
                }

               
            }            
            catch
            {

            }
        }

        private static bool CreateLogFile(string fileName)
        {
            try
            {
                File.CreateText(fileName).Close();
                return true;
            }
            catch(Exception ex)
            {
            }
            return false;
        }

        private static bool CheckIfLogFileExists(string fileName)
        {
            try
            {
                return File.Exists(fileName);

            }
            catch
            {
            }
            return false;
        }

        private static bool LogException(string fileName, Exception ex, string IPAddress = null, int? UserID = null)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(string.Empty);
                    sw.WriteLine("Date: "+DateTime.Now.ToString()+ "|IP: " + IPAddress+ "|UserID: " + UserID);
                    sw.WriteLine("ex: " + ex.ToString());
                    sw.WriteLine("ex.Message: " + ex.Message);
                    sw.WriteLine("ex.StackTrace: " + ex.StackTrace);
                    sw.WriteLine("ex.Source: " + ex.Source);
                    StringBuilder s = new StringBuilder();
                    while (ex != null)
                    {
                    s.AppendLine("Exception type: " + ex.GetType().FullName);
                        s.AppendLine("Message       : " + ex.Message);
                        s.AppendLine("Stacktrace:");
                        s.AppendLine(ex.StackTrace);
                        s.AppendLine();
                        ex = ex.InnerException;
                    }
                    sw.WriteLine("innerexceptions: " + s);

                    sw.WriteLine();
                    sw.WriteLine();

                }
                return true;
            }
            catch
            {
            }
            return false;
        }
    }


    public class NscReturn
    {
        public ActivityStatus status { get; set; }
        public string message { get; set; }
        public object returnData { get; set; }
    }

    public enum ActivityStatus{

        SUCCESS,
        ERROR,
        EXCEPTION,
        NOROWS,
        DUPLICATE,
        INVALID
    }


}