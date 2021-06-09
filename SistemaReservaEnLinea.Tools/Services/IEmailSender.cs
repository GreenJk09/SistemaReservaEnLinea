using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservaEnLinea.Tools.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
