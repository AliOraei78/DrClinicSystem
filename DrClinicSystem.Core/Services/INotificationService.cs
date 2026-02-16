// Core/Services/INotificationService.cs
public interface INotificationService
{
    Task SendAsync(string message, string recipient);
}