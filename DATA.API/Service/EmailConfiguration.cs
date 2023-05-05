using E_BOOK.API.Service.Service_Interface;
using System.Net.Mail;

namespace E_BOOK.API.Service
{
    public class EmailConfiguration
    {
       public string From { get; set; } = "ifemicheal2@gmail.com";
        public string SmtpServer { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 468;
        public string UserName { get; set; } = "ifemicheal2@gmail.com";
        public string Password { get; set; } = "hatgerkzusiwxysl";

    }

}
