
using System.Threading.Tasks;
    public interface IEmailSender
    {
        Task<string> SendEmailAsync(string recipientEmail, string recipientFirstName, string Subject, string Body);
    }

