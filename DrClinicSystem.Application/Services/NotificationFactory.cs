// Application/Services/NotificationFactory.cs
using Microsoft.Extensions.DependencyInjection;

public class NotificationFactory
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public INotificationService Create(string type)
    {
        return type.ToLowerInvariant() switch
        {
            "email" => _serviceProvider.GetRequiredKeyedService<INotificationService>("email"),
            "sms" => _serviceProvider.GetRequiredKeyedService<INotificationService>("sms"),
            _ => throw new ArgumentException($"Invalid notification type: {type}")
        };
    }
}