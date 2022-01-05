using Commons.Helper;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WebSiteAPI.Models
{
   public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(List<EmailConfiguration> to, string subject, string cont )
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x.DisplayName, x.Address)));
            Subject = subject;
            Content = cont;
        }
       
    }
}
