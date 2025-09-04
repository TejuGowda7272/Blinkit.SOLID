using System;


namespace Blinkit.SOLID.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine($"[LOG {DateTime.UtcNow:O}] {message}");
    }
}