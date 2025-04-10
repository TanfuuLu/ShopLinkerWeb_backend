using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using UserService.Repositories.Interfaces;

namespace UserService.Repositories;

public class EmailRepository : IEmailService {
	private readonly IConfiguration _configuration;

	public EmailRepository(IConfiguration configuration) {
		_configuration = configuration;
	}

	public async Task SendEmailAsync(string toEmail, string subject, string message) {
		var emailSettings = _configuration.GetSection("EmailSettings");
		var mimeMessage = new MimeMessage();
		mimeMessage.From.Add(new MailboxAddress(
		emailSettings["SenderName"],
		 emailSettings["SenderEmail"]));
		mimeMessage.To.Add(new MailboxAddress("", toEmail));
		mimeMessage.Subject = subject;

		var bodyBuilder = new BodyBuilder { HtmlBody = message };
		mimeMessage.Body = bodyBuilder.ToMessageBody();

		using var client = new SmtpClient();
		await client.ConnectAsync(
		    emailSettings["SmtpServer"],
		    int.Parse(emailSettings["SmtpPort"]),
		    SecureSocketOptions.StartTls);
		await client.AuthenticateAsync(
		    emailSettings["Username"],
		    emailSettings["Password"]);
		await client.SendAsync(mimeMessage);
		await client.DisconnectAsync(true);
	}
}
