using Electronic_Application.Models.Email;


namespace Electronic_Application.Contracts.Infastructure
{
    public interface IEmailService
    {
        //Task<bool> SendEmail(Email email, List<Attachment> attachments = null);
        Task SendEmailAsync(Email mailRequest);
    }
}
