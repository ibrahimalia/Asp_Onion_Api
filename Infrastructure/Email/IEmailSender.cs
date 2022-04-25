namespace Infrastructure.Email
{
    public interface IEmailSender
    {
             void SendEmail(Message message);
    }
}