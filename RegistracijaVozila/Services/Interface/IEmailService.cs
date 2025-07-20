namespace RegistracijaVozila.Services.Interface
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string toEmail, byte[] pdfBytes);
    }
}
