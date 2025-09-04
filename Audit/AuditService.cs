using BlinkItSOLIDPrinciples.Logging;
using System;
using System.Collections.Generic;

namespace BlinkItSOLIDPrinciples.Audit
{
    public class AuditService : IAuditService
    {
        private readonly List<string> _entries = new();
        private readonly ILogger _logger;

        public AuditService(ILogger logger)
        {
            _logger = logger;
        }

        public void Record(string message)
        {
            string logEntry = $"{DateTime.Now:HH:mm:ss} - {message}";
            _entries.Add(logEntry);

            // Optional: also log it
            _logger.Log($"[AUDIT] {message}");
        }

        public IReadOnlyList<string> Entries => _entries;
    }
}
