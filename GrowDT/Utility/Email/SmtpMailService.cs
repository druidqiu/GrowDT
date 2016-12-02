using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GrowDT.Application;
using GrowDT.Logging;

namespace GrowDT.Utility.Email
{
    public class SmtpMailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public SmtpMailService()
        {
            _emailSettings = new EmailSettings
            {
                FromDisplayName = AppConfig.SmtpUserDisplayName,
                Host = AppConfig.SmtpHost,
                HostPort = AppConfig.SmtpHostPort,
                FromAddress = AppConfig.SmtpUserAddress,
                Password = AppConfig.SmtpUserPwd,
                FileLocation = AppConfig.EmailFileLocation,
                UseSsl = AppConfig.SmtpUseSsl,
                WriteAsFile = AppConfig.EmailWriteAsFile,
                Timeout = 9999,
                InterceptReceiver = AppConfig.EmailInterceptReceiver
            };
        }

        public SmtpMailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public bool SendMail(List<string> tos, string subject, string body,
            EmailPriorityLevel priority = EmailPriorityLevel.High)
        {
            return SmtpSendMailIntercepter(tos, null, null, subject, body, null, null, priority);
        }

        public bool SendMail(List<string> tos, List<string> ccs, string subject, string body,
            EmailPriorityLevel priority = EmailPriorityLevel.High)
        {
            return SmtpSendMailIntercepter(tos, ccs, null, subject, body, null, null, priority);
        }

        public bool SendMail(List<string> tos, string subject, string body,
            List<EmailAttachment> attachments, EmailPriorityLevel priority = EmailPriorityLevel.High)
        {
            return SmtpSendMailIntercepter(tos, null, null, subject, body, null, attachments, priority);
        }

        public bool SendMail(List<string> tos, string subject, string body, List<EmailImageInline> images,
            EmailPriorityLevel priority = EmailPriorityLevel.High)
        {
            return SmtpSendMailIntercepter(tos, null, null, subject, body, images, null, priority);
        }

        public bool SendMail(List<string> tos, string subject, string body, List<EmailImageInline> images,
            List<EmailAttachment> attachments, EmailPriorityLevel priority = EmailPriorityLevel.High)
        {
            return SmtpSendMailIntercepter(tos, null, null, subject, body, images, attachments, priority);
        }

        public bool SendMail(List<string> tos, List<string> ccs, string subject, string body, List<EmailImageInline> images, List<EmailAttachment> attachments,
            EmailPriorityLevel priority = EmailPriorityLevel.High)
        {
            return SmtpSendMailIntercepter(tos,ccs, null, subject, body, images, attachments, priority);
        }
        public bool SendMail(List<string> tos, List<string> ccs, List<string> bccs,string subject, string body, List<EmailImageInline> images, List<EmailAttachment> attachments,
            EmailPriorityLevel priority = EmailPriorityLevel.High)
        {
            return SmtpSendMailIntercepter(tos, ccs, bccs, subject, body, images, attachments, priority);
        }

        public List<CustomMailMessage> SendMail(List<CustomMailMessage> mailMessages)
        {
            var errorMails = new List<CustomMailMessage>();

            var parallelOption = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount * 2
            };
            Parallel.ForEach(mailMessages, parallelOption, mailMessage =>
            {
                if (mailMessage == null) return;

                var to = mailMessage.ToAddress.Split(';').ToList();

                if (!SmtpSendMailIntercepter(to, null, null, mailMessage.Subject, mailMessage.Body, null, null,
                    EmailPriorityLevel.High))
                {
                    errorMails.Add(mailMessage);
                }
            });

            return errorMails;
        }

        private bool SmtpSendMailIntercepter(List<string> tos, List<string> ccs, List<string> bccs, string subject,
            string body,
            List<EmailImageInline> imgInlines, List<EmailAttachment> attachments, EmailPriorityLevel priority)
        {
            if (_emailSettings.IsIntercepted)
            {
                var newTos = _emailSettings.InterceptReceiver.Split(';').ToList();

                string newBody = body + "<br/>";
                if (tos != null)
                {
                    newBody += "<br/>to: " + string.Join(",", tos.ToArray());
                }
                if (ccs != null)
                {
                    newBody += "<br/>cc: " + string.Join(",", ccs.ToArray());
                }
                if (bccs != null)
                {
                    newBody += "<br/>bcc: " + string.Join(",", bccs.ToArray());
                }

                return SmtpSendMail(newTos, null, null, subject, newBody, imgInlines, attachments, priority);
            }
            else
            {
                return SmtpSendMail(tos, ccs, bccs, subject, body, imgInlines, attachments, priority);
            }
        }

        private bool SmtpSendMail(List<string> tos, List<string> ccs, List<string> bccs, string subject, string body,
            List<EmailImageInline> imgInlines, List<EmailAttachment> attachments, EmailPriorityLevel priority)
        {
            if (tos == null || tos.Count == 0 || !tos.Any(t => t.Contains("@")))
            {
                throw new Exception("mail to is null");
            }

            MailMessage mailMessage = new MailMessage();

            #region prepare mail address

            mailMessage.From = new MailAddress(_emailSettings.FromAddress, _emailSettings.FromDisplayName);

            tos.Where(t => t.Contains("@")).Select(t=>t.Trim()).Distinct().ToList().ForEach(to =>
            {
                mailMessage.To.Add(new MailAddress(to));
            });

            if (ccs != null)
            {
                ccs.Where(t => t.Contains("@")).Select(t => t.Trim()).Distinct().ToList().ForEach(cc =>
                {
                    mailMessage.CC.Add(new MailAddress(cc));
                });
            }

            if (bccs != null)
            {
                bccs.Where(t => t.Contains("@")).Select(t => t.Trim()).Distinct().ToList().ForEach(bcc =>
                {
                    mailMessage.Bcc.Add(new MailAddress(bcc));
                });
            }

            #endregion prepare mail address

            #region prepare mail content

            mailMessage.Subject = subject;
            mailMessage.Body = body;

            if (imgInlines != null)
            {
                var htmlBody = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                foreach (var item in imgInlines)
                {
                    var lkImg = string.IsNullOrEmpty(item.FileName)
                        ? new LinkedResource(item.ContentStream)
                        : new LinkedResource(item.FileName);
                    lkImg.ContentId = item.ContentId;
                    htmlBody.LinkedResources.Add(lkImg);
                }
                mailMessage.AlternateViews.Add(htmlBody);
            }

            if (attachments != null)
            {
                foreach (var item in attachments)
                {
                    mailMessage.Attachments.Add(string.IsNullOrEmpty(item.FileName)
                        ? new Attachment(item.ContentStream, item.Name)
                        : new Attachment(item.FileName));
                }
            }

            #endregion prepare mail content

            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = (MailPriority) (int) priority;
            try
            {
                SmtpSendMail(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                LoggingFactory.GetLogger().Log(LogLevel.Error, "Send mail error:", ex);
                return false;
            }
        }

        private void SmtpSendMail(MailMessage mailMessage)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = _emailSettings.UseSsl;
                smtpClient.Host = _emailSettings.Host;
                smtpClient.Port = _emailSettings.HostPort;
                smtpClient.Timeout = _emailSettings.Timeout;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(_emailSettings.FromAddress,
                        _emailSettings.Password);
                if (_emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation =
                        _emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                smtpClient.Send(mailMessage);
            }
        }
    }
}
