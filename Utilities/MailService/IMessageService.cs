using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Utilities.MailService
{
    public interface IMessageService
    {
         Task SendEmailAsync(string toName, 
            string toEmailAdress,
            string subject,
            string message);
    }
}
