// Infrastructure/Services/EmailNotificationService.cs
public class EmailNotificationService : INotificationService
{
    public Task SendAsync(string message, string recipient)
    {
        Console.WriteLine($"[Email] Sending to {recipient}: {message}");
        return Task.CompletedTask;
    }
}