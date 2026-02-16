// Infrastructure/Services/SmsNotificationService.cs
public class SmsNotificationService : INotificationService
{
    public Task SendAsync(string message, string recipient)
    {
        Console.WriteLine($"[SMS] Sending to {recipient}: {message}");
        return Task.CompletedTask;
    }
}