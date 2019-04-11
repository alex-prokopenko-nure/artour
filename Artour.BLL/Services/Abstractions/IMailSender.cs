using Artour.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Artour.BLL.Services.Abstractions
{
    public interface IMailSender
    {
        Task SendRecoveryLink(String recipientEmail, String temporaryPassword, String userNickName, String recoveryLink);

        Task SendConfirmationEmail(UserViewModel userViewModel, String verificationLink);
    }
}
