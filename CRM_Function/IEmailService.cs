using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Function
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string sellerName, string subject, string body);
    }
}
