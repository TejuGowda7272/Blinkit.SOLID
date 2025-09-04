using Blinkit.SOLID.Logging;
using Blinkit.SOLID.Audit;


namespace Blinkit.SOLID.Audit
{
// SRP: tracks audit events separately from logging
public class AuditService : IAuditService
{
private readonly ILogger _logger;
public AuditService(ILogger logger) { _logger = logger; }
public void Record(string message) => _logger.Log("[AUDIT] " + message);
}
}