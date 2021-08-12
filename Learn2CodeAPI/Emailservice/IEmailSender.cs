using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emailservice
{
    public interface IEmailSender
    {
       
        Task SendEmailAsync(Message message);
    }
}
