using BlinkItSOLIDPrinciples.Logging;

namespace BlinkItSOLIDPrinciples.Notifications
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        public EmailSender(ILogger logger) { _logger = logger; }
        public void Send(string to, string subject, string body) => _logger.Log($"Email to {to}: {subject} / {body}");
    }
}