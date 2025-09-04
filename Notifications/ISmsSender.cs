namespace Blinkit.SOLID.Notifications
{
    public interface ISmsSender { void Send(string to, string message); }
}