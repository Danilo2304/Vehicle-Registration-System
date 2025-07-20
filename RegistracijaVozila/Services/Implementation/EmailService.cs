using MailKit.Security;
using MimeKit;
using RegistracijaVozila.Services.Interface;
using MailKit.Net.Smtp;

namespace RegistracijaVozila.Services.Implementation
{
    public class EmailService : IEmailService
    {
        public async Task SendConfirmationEmailAsync(string toEmail, byte[] pdfBytes)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("jankovic.danilo23@gmail.com"));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = "Potvrda o registraciji vozila";

            var builder = new BodyBuilder
            {
                TextBody = "Poštovani,\n\nU prilogu se nalazi potvrda o registraciji vašeg vozila." +
                "\n\nSrdačan pozdrav,\nMinistarstvo unutrašnjih poslova Republike Srbije"
            };

            builder.Attachments.Add("PotvrdaRegistacije.pdf",pdfBytes,new ContentType("application", "pdf"));
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("jankovic.danilo23@gmail.com", "twby lpec aotj fyrb");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
