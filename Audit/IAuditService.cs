using System.Collections.Generic;

namespace BlinkItSOLIDPrinciples.Audit
{
    public interface IAuditService
    {
        void Record(string message);

        // Expose read-only audit log
        IReadOnlyList<string> Entries { get; }
    }
}
