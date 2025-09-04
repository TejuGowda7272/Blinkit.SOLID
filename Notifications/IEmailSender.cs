namespace BlinkItSOLIDPrinciples.Notifications
{
    public interface IEmailSender { void Send(string to, string subject, string body); }
}