using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace ShradhaBook_API.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public void SendEmail(EmailDto request)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("eproject@mail.com"));
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject = "Test email Subject";
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = request.Body
        };

        using var smtp = new SmtpClient();
        smtp.Connect(_config.GetSection("Email:EmailHost").Value, 2525, SecureSocketOptions.StartTls);
        smtp.Authenticate(_config.GetSection("Email:EmailUsername").Value,
            _config.GetSection("Email:EmailPassword").Value);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}