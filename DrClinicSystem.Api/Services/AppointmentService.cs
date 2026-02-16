using Microsoft.Extensions.Options;
using DrClinicSystem.Core.Services; // ← Add this using (for IAppointmentService)
using DrClinicSystem.Core.Entities; // ← For Appointment (if used)


public class AppointmentService : IAppointmentService
{
    private readonly ClinicOptions _options;

    public AppointmentService(IOptions<ClinicOptions> options)
    {
        _options = options.Value;
    }

    public void ScheduleAppointment()
    {
        Console.WriteLine($"Clinic: {_options.ClinicName}, Duration: {_options.AppointmentDuration.TotalMinutes} min");
    }

    // If the interface has additional methods, implement them here
    public Task ScheduleAsync(Appointment appointment)
    {
        // Actual appointment scheduling logic
        Console.WriteLine($"Scheduling for {appointment.PatientName} at {appointment.DateTime}");
        return Task.CompletedTask;
    }
}
