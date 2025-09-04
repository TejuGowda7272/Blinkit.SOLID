using BlinkItSOLIDPrinciples.Logging;

namespace BlinkItSOLIDPrinciples.Notifications
{
    public class SmsSender : ISmsSender
    {
        private readonly ILogger _logger;
        public SmsSender(ILogger logger) { _logger = logger; }
        public void Send(string to, string message) => _logger.Log($"SMS to {to}: {message}");
    }
}