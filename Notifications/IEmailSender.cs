namespace Blinkit.SOLID.Notifications
{
    public interface IEmailSender { void Send(string to, string subject, string body); }
}