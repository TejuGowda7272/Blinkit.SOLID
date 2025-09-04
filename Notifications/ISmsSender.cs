namespace BlinkItSOLIDPrinciples.Notifications
{
    public interface ISmsSender { void Send(string to, string message); }
}