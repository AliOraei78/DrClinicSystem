// Core/Services/INotificationChannel.cs
public interface INotificationChannel
{
    Task SendAsync(string message);
}