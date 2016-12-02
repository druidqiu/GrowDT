namespace GrowDT.MvcHelper.Utility
{
    public class FileUploadResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public long TotalMilliseconds { get; set; }

        public override string ToString()
        {
            return Success ? string.Format("导入成功。<br/>本次导入共用时{0}秒。", TotalMilliseconds/1000) : string.Format("导入未成功，原因如下：<br/>{0}", ErrorMessage);
        }
    }
}
