using System.Net;
using System.Net.Mail;
using System.Text;

namespace SoftwareTesting.Mocking
{
    public interface IEmailSender
    {
        void EmailFile(string emailAddress, string emailBody, string fileName, string subject);
    }

    public class EmailSender : IEmailSender
    {
        public void EmailFile(string emailAddress, string emailBody, string fileName, string subject)
        {
            var client = new SmtpClient(SystemSettingsHelper.EmailSmtpHost)
            {
                Port = SystemSettingsHelper.EmailPort,
                Credentials =
                    new NetworkCredential(
                        SystemSettingsHelper.EmailUsername,
                        SystemSettingsHelper.EmailPassword)
            };

            var from = new MailAddress(SystemSettingsHelper.EmailFromEmail, SystemSettingsHelper.DisplayName,
                Encoding.UTF8);
            var to = new MailAddress(emailAddress);

            var message = new MailMessage(from, to)
            {
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
                Body = emailBody,
                BodyEncoding = Encoding.UTF8
            };

            message.Attachments.Add(new Attachment(fileName));
            client.Send(message);
            message.Dispose();

            File.Delete(fileName);

        }
    }

    public class SystemSettingsHelper
    {
        public static string EmailSmtpHost { get; set; }
        public static int EmailPort { get; set; }
        public static string EmailUsername { get; set; }
        public static string EmailPassword { get; set; }
        public static string EmailFromEmail { get; set; }
        public static string DisplayName { get; set; }
    }
}

