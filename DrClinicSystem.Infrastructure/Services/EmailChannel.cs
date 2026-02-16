using System;
using System.Threading.Tasks;
using DrClinicSystem.Core.Services;

namespace DrClinicSystem.Core.Services;

public class EmailChannel : INotificationChannel
{
    public Task SendAsync(string message)
    {
        Console.WriteLine($"[EMAIL CHANNEL] Sending: {message}");

        return Task.CompletedTask;
    }
}