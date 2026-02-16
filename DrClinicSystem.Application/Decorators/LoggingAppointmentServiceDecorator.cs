// Application/Decorators/LoggingAppointmentServiceDecorator.cs
using Microsoft.Extensions.Logging;                     
using DrClinicSystem.Core.Entities;                    
using DrClinicSystem.Core.Services;                    
using System.Threading.Tasks;
public class LoggingAppointmentServiceDecorator : IAppointmentService
{
    private readonly IAppointmentService _inner;
    private readonly ILogger<LoggingAppointmentServiceDecorator> _logger;

    public LoggingAppointmentServiceDecorator(
        IAppointmentService inner,
        ILogger<LoggingAppointmentServiceDecorator> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task ScheduleAsync(Appointment appointment)
    {
        _logger.LogInformation("Scheduling appointment for {Patient}", appointment.PatientName);
        await _inner.ScheduleAsync(appointment);
        _logger.LogInformation("Appointment scheduled successfully");
    }
}