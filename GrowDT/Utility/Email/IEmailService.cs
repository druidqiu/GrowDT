using System.Collections.Generic;
using System.IO;

namespace GrowDT.Utility.Email
{
    public enum EmailPriorityLevel
    {
        Normal,
        Low,
        High,
    }

    public interface IEmailService
    {
        bool SendMail(List<string> tos, string subject, string body,
            EmailPriorityLevel priority = EmailPriorityLevel.High);

        bool SendMail(List<string> tos, List<string> ccs, string subject, string body,
            EmailPriorityLevel priority = EmailPriorityLevel.High);
        bool SendMail(List<string> tos, string subject, string body,
    List<EmailAttachment> attachments, EmailPriorityLevel priority = EmailPriorityLevel.High);
        bool SendMail(List<string> tos, string subject, string body, List<EmailImageInline> images,
            EmailPriorityLevel priority = EmailPriorityLevel.High);        
        bool SendMail(List<string> tos, string subject, string body, List<EmailImageInline> images,
            List<EmailAttachment> attachments, EmailPriorityLevel priority = EmailPriorityLevel.High);
        bool SendMail(List<string> tos, List<string> ccs, string subject, string body, List<EmailImageInline> images,
            List<EmailAttachment> attachments, EmailPriorityLevel priority = EmailPriorityLevel.High);
        bool SendMail(List<string> tos, List<string> ccs,List<string> bccs , string subject, string body, List<EmailImageInline> images,
            List<EmailAttachment> attachments, EmailPriorityLevel priority = EmailPriorityLevel.High);
        List<CustomMailMessage> SendMail(List<CustomMailMessage> mails);
    }

    public class EmailAttachment
    {
        public EmailAttachment(string fileName)
        {
            FileName = fileName;
        }

        public EmailAttachment(Stream contentStream, string name)
        {
            ContentStream = contentStream;
            Name = name;
        }

        public string FileName { get; private set; }
        public Stream ContentStream { get; private set; }
        public string Name { get; private set; }
    }

    public class EmailImageInline
    {
        public EmailImageInline(string contentId, string fileName)
        {
            ContentId = contentId;
            FileName = fileName;
        }

        public EmailImageInline(string contentId, Stream contentStream)
        {
            ContentId = contentId;
            ContentStream = contentStream;
        }

        public string ContentId { get; private set; }
        public string FileName { get; private set; }
        public Stream ContentStream { get; private set; }
    }

    public class EmailSettings
    {
        public string FromAddress { get; set; }
        public string FromDisplayName { get; set; }
        public bool UseSsl { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int HostPort { get; set; }
        public bool WriteAsFile { get; set; }
        public string FileLocation { get; set; }
        public int Timeout { get; set; }
        public string InterceptReceiver { get; set; }
        public bool IsIntercepted {
            get { return !string.IsNullOrWhiteSpace(InterceptReceiver); }
        }
    }

    public class CustomMailMessage
    {
        /// <param name="toAddress">多个地址用;分隔</param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public CustomMailMessage(string toAddress,string subject,string body)
        {
            ToAddress = toAddress;
            Subject = subject;
            Body = body;
        }

        public string ToAddress { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
    }
}
