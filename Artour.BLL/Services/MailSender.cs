using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Artour.BLL.Services.Abstractions;
using Artour.BLL.Models;
using Artour.BLL.ViewModels;
using Artour.BLL.Common;
using Microsoft.Extensions.Configuration;

namespace Artour.BLL.Services
{
    public class MailSender : IMailSender
    {
        private const String PASSWORD_RECOVERY_SUBJECT = "Artour password recovery";
        private const String EMAIL_CONFIRMATION_SUBJECT = "Artour account confirmation";

        private readonly SmtpConfiguration _configuration;

        public MailSender(IConfiguration configuration)
        {
            _configuration = new SmtpConfiguration();
            configuration.Bind(nameof(SmtpConfiguration), _configuration);
        }

        public async Task SendRecoveryLink(String recipientEmail, String temporaryPassword, String userNickName, String recoveryLink)
        {
            var message = new MailMessage(new MailAddress(_configuration.Login, "Artour"), new MailAddress(recipientEmail));
            String fullPath = System.Reflection.Assembly.GetAssembly(GetType()).Location;
            String currentDirectory = Path.GetDirectoryName(fullPath);

            message.Body = await ReadMailTemplate(EmailTemplateNames.PASSWORD_RECOVERY, userNickName, temporaryPassword, recoveryLink);
            message.Subject = PASSWORD_RECOVERY_SUBJECT;
            message.IsBodyHtml = false;

            await SendMail(message);
        }

        public async Task SendConfirmationEmail(UserViewModel userViewModel, String verificationLink)
        {
            var message = new MailMessage(new MailAddress(_configuration.Login, "Artour"), new MailAddress(userViewModel.Email));
            String fullPath = System.Reflection.Assembly.GetAssembly(GetType()).Location;
            String currentDirectory = Path.GetDirectoryName(fullPath);

            message.Body = await ReadMailTemplate(EmailTemplateNames.EMAIL_CONFIRMATION, userViewModel.Username, verificationLink);
            message.Subject = EMAIL_CONFIRMATION_SUBJECT;
            message.IsBodyHtml = true;

            await SendMail(message);
        }

        private String GetEmailTemplateFileFullPath(String templateName)
        {
            String fullPath = System.Reflection.Assembly.GetAssembly(GetType()).Location;
            String currentDirectory = Path.GetDirectoryName(fullPath);

            return Path.Combine(currentDirectory, _configuration.EmailTemplateFolder, templateName);
        }

        private async Task<String> ReadMailTemplate(String templateName, params Object[] templateFillData)
        {
            var fileName = GetEmailTemplateFileFullPath(templateName);

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"Cannot find email template filename: {fileName}");
            }

            using (var reader = new StreamReader(fileName))
            {
                var mailBody = await reader.ReadToEndAsync();

                return String.Format(mailBody, templateFillData);
            }
        }

        private async Task SendMail(MailMessage mailMessage)
        {
            using (var smtpClient = new SmtpClient(_configuration.Host, _configuration.Port))
            {
                mailMessage.IsBodyHtml = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_configuration.Login, _configuration.Password);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}

