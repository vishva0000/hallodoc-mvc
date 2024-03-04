
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLayer.Utility
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string v1, string v2, string v3);
    }
}


