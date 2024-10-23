using MindMaze.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MindMaze.Infrastructure.infrastructure.Data;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using MailKit.Net.Smtp;

namespace MindMaze.Infrastructure.infrastructure.Services.EmailMessages
{
    public class EmailService : IEmailService
    {

        private readonly ApplicationDBContext _dbcontext;

        private readonly EmailOptions _emailOptions;

        public EmailService(ApplicationDBContext dbcontext, IOptions<EmailOptions> options)
        {
            _dbcontext = dbcontext;
            _emailOptions = options.Value;
        }

        public Task SendEmailtoGmail(string email)
        {
            var message = new MimeMessage();

            message.From.Add(MailboxAddress.Parse(_emailOptions.Gmail));

            message.From.Add(MailboxAddress.Parse(email));

            message.To.Add(MailboxAddress.Parse(email));

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
               Text =  _emailOptions.VerificationMessage
            };


            var client = new MailKit.Net.Smtp.SmtpClient();

            client.Connect(_emailOptions.Gmail, _emailOptions.GmailPort, MailKit.Security.SecureSocketOptions.StartTls);

            client.Authenticate(_emailOptions.ServerGmail, _emailOptions.ServerGmailPassword);

            client.Send(message);

            client.Disconnect(true);


            return Task.CompletedTask;


        }
    }
}
