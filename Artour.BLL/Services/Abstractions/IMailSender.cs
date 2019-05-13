using Artour.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Artour.BLL.Services.Abstractions
{
    public interface IMailSender
    {
        Task SendAccessCode(String recipientEmail, String accessCode);

        Task SendConfirmationEmail(UserViewModel userViewModel, String verificationLink);
    }
}
