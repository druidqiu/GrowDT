using GrowDT.MvcHelper.Utility;
using System.Diagnostics;
using GrowDT.Application;

namespace System.Web.Mvc
{
    public static class FileContentHelper
    {
        public static FileUploadResult UploadExcelAndSaveData(this HttpRequestBase request, string fileFlag, Func<string, string> importDataFunc)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = UploadExcelFile(request, fileFlag, importDataFunc);   
            stopwatch.Stop();
            result.TotalMilliseconds = stopwatch.ElapsedMilliseconds;
            return result;
        }

        private static FileUploadResult UploadExcelFile(HttpRequestBase request, string fileFlag, Func<string, string> importDataFunc)
        {
            if (request.Files.Count == 0)
            {
                return new FileUploadResult {Success = false, ErrorMessage = "没有找到文件"};
            }

            HttpPostedFileBase file = request.Files[0];
            if (file == null || file.ContentLength == 0)
            {
                return new FileUploadResult {Success = false, ErrorMessage = "文件没有数据"};
            }

            if (!FileHelper.IsExcel(file.FileName))
            {
                return new FileUploadResult { Success = false, ErrorMessage = "不是有效的Excel文件" };
            }

            string serverFilePath = FileHelper.UploadFile(file, AppConfig.ExcelUploadFolder, fileFlag);
            if (string.IsNullOrEmpty(serverFilePath))
            {
                return new FileUploadResult
                {
                    Success = false,
                    ErrorMessage = "文件上传过程发生异常"
                };
            }

            string importErrorMsg = importDataFunc(serverFilePath);

            if (!string.IsNullOrEmpty(importErrorMsg))
            {
                IO.File.Delete(serverFilePath);
                return new FileUploadResult
                {
                    Success = false,
                    ErrorMessage = importErrorMsg,
                };
            }

            return new FileUploadResult
            {
                Success = true,
                ErrorMessage = "",
            };
        }
    }
}
