using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ahsan.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Ahsan.Service.Helpers;

public class EmailVerification
{
    private readonly IConfiguration configuration;
    public EmailVerification(IConfiguration configuration)
    {
        this.configuration = configuration.GetSection("Email");
    }
    public async Task<string> SendAsync(User user)
    {
        try
        {
            Random random = new Random();
            int verificationCode = random.Next(123456, 999999);

            ConnectionMultiplexer resdisConnect = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = resdisConnect.GetDatabase();
            db.StringSet("code", verificationCode.ToString());
            var result = db.StringGet("code");

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(this.configuration["EmailAddress"]));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Email verification Ahsan.uz";
            email.Body = new TextPart(TextFormat.Html) { Text = result.ToString() };

            var sendMessage = new MailKit.Net.Smtp.SmtpClient();
            await sendMessage.ConnectAsync(this.configuration["Host"], 587, SecureSocketOptions.StartTls);
            await sendMessage.AuthenticateAsync(this.configuration["EmailAddress"], this.configuration["Password"]);
            await sendMessage.SendAsync(email);
            await sendMessage.DisconnectAsync(true);

            return result.ToString();
        }
        catch
        {
            return null;
        }
    }
}
