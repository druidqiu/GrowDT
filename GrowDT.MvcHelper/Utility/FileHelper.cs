using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using GrowDT.Timing;
using System.Linq;

namespace GrowDT.MvcHelper.Utility
{
    public static class FileHelper
    {
        public static string UploadFile(HttpPostedFileBase fileToUpload, string folder, string fileFlag)
        {
            if (fileToUpload == null || fileToUpload.ContentLength <= 0)
            {
                return string.Empty;
            }
            var serverFileName = string.Format("Im_{0}_{1}{2}",
                string.IsNullOrEmpty(fileFlag) ? Guid.NewGuid().ToString() : fileFlag,
                Clock.Now.ToString("yyyyMMddHHmmss"),
                Path.GetExtension(fileToUpload.FileName));

            var serverFilePath = Path.Combine(folder, serverFileName);
            fileToUpload.SaveAs(serverFilePath);

            return serverFilePath;
        }

        public static string GetFullFilePath(string path)
        {
            //TODO:需要重构
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory.ToLower();
            if (path.ToLower().StartsWith(baseDirectory))
                return path;
            return Path.Combine(baseDirectory, path);
        }

        public static bool IsExcel(string filename)
        {
            return CheckFileType(filename, new[] {".xls", ".xlsx"});
        }

        public static bool IsWord(string filename)
        {
            return CheckFileType(filename, new[] { ".doc", ".docx" });
        }

        public static bool IsImage(string filename)
        {
            return CheckFileType(filename, new[] {".jpeg", ".jpg", ".bmp", ".png", ".gif"});
        }

        public static bool IsPackage(string filename)
        {
            return CheckFileType(filename, new[] {".zip", ".rar", ".7z"});
        }

        public static bool IsPdf(string filename)
        {
            return CheckFileType(filename, new[] {".pdf"});
        }

        private static bool CheckFileType(string filename, IEnumerable<string> extensions)
        {
            string ext = Path.GetExtension(filename);
            if (string.IsNullOrEmpty(ext))
            {
                return false;
            }

            return extensions.Any(t => t.ToUpper() == ext.ToUpper());
        }

        public static string GetFileType(string filename)
        {
            string type;
            string ext = Path.GetExtension(filename);
            if (string.IsNullOrEmpty(ext))
            {
                return string.Empty;
            }

            switch (ext.ToUpper())
            {
                case ".PDF":
                    type = "application/pdf";
                    break;
                case ".JPEG":
                    type = "image/jpeg";
                    break;
                case ".JPG":
                    type = "image/jpeg";
                    break;
                case ".PNG":
                    type = "image/png";
                    break;
                case ".GIF":
                    type = "image/gif";
                    break;
                case ".ZIP":
                    type = "application/zip";
                    break;
                case ".TIFF":
                    type = "image/tiff";
                    break;
                case ".TIF":
                    type = "image/tif";
                    break;
                case ".DOC":
                    type = "application/msword";
                    break;
                case ".DOCX":
                    type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case ".XLS":
                    type = "application/vnd.ms-excel";
                    break;
                case ".XLSX":
                    type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case ".PPT":
                    type = "application/vnd.ms-powerpoint";
                    break;
                case ".PPTX":
                    type = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    break;
                case ".TXT":
                    type = "text/plain";
                    break;
                case ".XLSM":
                    type = "application/vnd.ms-excel.sheet.macroEnabled.12";
                    break;
                case ".XLTM":
                    type = "application/vnd.ms-excel.template.macroEnabled.12";
                    break;
                case ".XLTX":
                    type = "application/vnd.openxmlformats-officedocument.spreadsheetml.template";
                    break;
                case ".XLAM":
                    type = "application/vnd.ms-excel.addin.macroEnabled.12";
                    break;
                case ".XLSB":
                    type = "application/vnd.ms-excel.sheet.binary.macroEnabled.12";
                    break;
                case ".DOTX":
                    type = "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
                    break;
                case ".DOCM":
                    type = "application/vnd.ms-word.document.macroEnabled.12";
                    break;
                case ".DOTM":
                    type = "application/vnd.ms-word.template.macroEnabled.12";
                    break;
                default:
                    type = "application/octet-stream";
                    break;
            }
            return type;
        }

        public static string GetContentTypeBy(string filename, out bool shouldDownload)
        {
            shouldDownload = true;
            if (string.IsNullOrEmpty(filename))
            {
                return string.Empty;
            }

            string extUpper = Path.GetExtension(filename).ToUpper();

            switch (extUpper)
            {
                case ".PDF":
                case ".JPEG":
                case ".JPG":
                case ".PNG":
                case ".GIF":
                case ".TXT":
                    shouldDownload = false;
                    break;
            }

            return GetFileType(filename);
        }
    }
}
