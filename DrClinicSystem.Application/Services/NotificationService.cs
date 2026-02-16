using DrClinicSystem.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DrClinicSystem.Application.Services;

public class NotificationService
{
    private readonly INotificationChannel _emailChannel;
    private readonly INotificationChannel _smsChannel;

    public NotificationService(
        [FromKeyedServices("email")] INotificationChannel emailChannel,
        [FromKeyedServices("sms")] INotificationChannel smsChannel)
    {
        _emailChannel = emailChannel;
        _smsChannel = smsChannel;
    }

    public async Task NotifyPatientAsync(string message, string preferredChannel = "email")
    {
        INotificationChannel channel = preferredChannel.ToLowerInvariant() switch
        {
            "sms" => _smsChannel,
            _ => _emailChannel
        };

        await channel.SendAsync(message);
    }
}