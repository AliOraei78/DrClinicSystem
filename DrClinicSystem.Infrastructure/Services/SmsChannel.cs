using System;
using System.Threading.Tasks;
using DrClinicSystem.Core.Services;

namespace DrClinicSystem.Infrastructure.Services;

public class SmsChannel : INotificationChannel
{
    public Task SendAsync(string message)
    {
        Console.WriteLine($"[SMS CHANNEL] Sending: {message}");

        return Task.CompletedTask;
    }
}