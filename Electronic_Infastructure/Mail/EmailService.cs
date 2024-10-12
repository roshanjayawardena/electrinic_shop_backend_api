using Electronic_Application.Contracts.Infastructure;
using Electronic_Application.Models.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mime;

namespace Electronic_Infastructure.Mail
{
    public class EmailService: IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = mailSettings.Value;
            _logger = logger;
        }    
     
        //public async Task SendEmailAsync(Mailrequest mailrequest)
        //{
        //    var email = new MimeMessage();
        //    email.Sender = MailboxAddress.Parse(emailSettings.Email);
        //    email.To.Add(MailboxAddress.Parse(mailrequest.ToEmail));
        //    email.Subject = mailrequest.Subject;
        //    var builder = new BodyBuilder();


        //    byte[] fileBytes;
        //    if (System.IO.File.Exists("Attachment/dummy.pdf"))
        //    {
        //        FileStream file = new FileStream("Attachment/dummy.pdf", FileMode.Open, FileAccess.Read);
        //        using (var ms = new MemoryStream())
        //        {
        //            file.CopyTo(ms);
        //            fileBytes = ms.ToArray();
        //        }
        //        builder.Attachments.Add("attachment.pdf", fileBytes, ContentType.Parse("application/octet-stream"));
        //        builder.Attachments.Add("attachment2.pdf", fileBytes, ContentType.Parse("application/octet-stream"));
        //    }

        //    builder.HtmlBody = mailrequest.Body;
        //    email.Body = builder.ToMessageBody();

        //    using var smtp = new SmtpClient();
        //    smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
        //    smtp.Authenticate(emailSettings.Email, emailSettings.Password);
        //    await smtp.SendAsync(email);
        //    smtp.Disconnect(true);
        //}


        public async Task SendEmailAsync(Email mailRequest)
        {
            try
            {
               using (MimeMessage emailMessage = new MimeMessage())
                {
                    MailboxAddress emailFrom = new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail);
                    emailMessage.From.Add(emailFrom);
                    MailboxAddress emailTo = new MailboxAddress(mailRequest.To, mailRequest.EmailToId);
                    emailMessage.To.Add(emailTo);

                    //emailMessage.Cc.Add(new MailboxAddress("Cc Receiver", "cc@example.com"));
                    //emailMessage.Bcc.Add(new MailboxAddress("Bcc Receiver", "bcc@example.com"));

                    emailMessage.Subject = mailRequest.Subject;

                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    byte[] fileBytes;

                    //var attachment = new MimePart()
                    //{
                    //    Content = new MimeContent(new MemoryStream(Encoding.UTF8.GetBytes(attachmentContent))),
                    //    ContentDisposition = new MimeKit.ContentDisposition(MimeKit.ContentDisposition.Attachment),
                    //    ContentTransferEncoding = ContentEncoding.Base64,
                    //    FileName = "database-data.csv"
                    //};

                    foreach (var file in mailRequest.Attachments)
                        {
                            if (file.Length > 0)
                            {
                                using (var ms = new MemoryStream())
                                {
                                    file.CopyTo(ms);
                                    fileBytes = ms.ToArray();
                                }
                            emailBodyBuilder.Attachments.Add(file.FileName, fileBytes, MimeKit.ContentType.Parse(MediaTypeNames.Application.Pdf));
                            }
                        }                   

                    emailBodyBuilder.TextBody = mailRequest.Body;

                    emailMessage.Body = emailBodyBuilder.ToMessageBody();
                    //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        await mailClient.ConnectAsync(_emailSettings.Server, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                        await mailClient.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
                        await mailClient.SendAsync(emailMessage);
                        await mailClient.DisconnectAsync(true);
                        _logger.LogInformation("Email sent.");
                    }
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError("Email sending failed.");

            }
        }

        //public async Task<bool> SendEmail(Email email, List<Attachment> attachments = null)
        //{

        //    var client = new SendGridClient(_emailSettings.ApiKey);

        //    var subject = email.Subject;
        //    var to = new EmailAddress(email.To);
        //    var emailBody = email.Body;

        //    var from = new EmailAddress
        //    {
        //        Email = _emailSettings.FromEmail,
        //        Name = _emailSettings.FromName
        //    };

        //    var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);

        //    if (attachments != null)
        //    {
        //        sendGridMessage.AddAttachments(attachments);
        //    }

        //    var response = await client.SendEmailAsync(sendGridMessage);

        //    _logger.LogInformation("Email sent.");

        //    if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
        //        return true;

        //    _logger.LogError("Email sending failed.");

        //    return false;
        //}
    }
}
