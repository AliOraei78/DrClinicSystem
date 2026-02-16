using DrClinicSystem.Application.Services;
using DrClinicSystem.Core.Entities;
using DrClinicSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DrClinicSystem.Api.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly NotificationFactory _notificationFactory;
    private readonly NotificationService _notificationService;
    private readonly AppointmentService _appointmentService;
    private readonly ClinicOptions _clinicOptions;

    public NotificationController(NotificationFactory notificationFactory, NotificationService notificationService, IOptions<ClinicOptions> options, AppointmentService appointmentService)
    {
        _notificationFactory = notificationFactory;
        _notificationService = notificationService;
        _appointmentService = appointmentService;
        _clinicOptions = options.Value;
    }

    /// <summary>
    /// Send a notification (Email or SMS) for appointment reminder
    /// </summary>
    /// <param name="type">Notification type: "email" or "sms"</param>
    /// <param name="request">Message and recipient information</param>
    /// <returns>Operation result</returns>
    [HttpPost("send")]
    public async Task<IActionResult> SendNotification(
        [FromQuery] string type,
        [FromBody] SendNotificationRequest request)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            return BadRequest("Notification type (type) is required. Allowed values: email or sms");
        }

        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("Message text is required");
        }

        if (string.IsNullOrWhiteSpace(request.Recipient))
        {
            return BadRequest("Recipient is required");
        }

        try
        {
            var notificationService = _notificationFactory.Create(type.ToLowerInvariant());

            await notificationService.SendAsync(request.Message, request.Recipient);

            return Ok(new
            {
                Message = "Notification sent successfully",
                Type = type,
                Recipient = request.Recipient
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // In a real environment, log the error and do not return internal details
            return StatusCode(500, "An error occurred while sending the notification");
        }
    }

    [HttpPost("notify")]
    public async Task<IActionResult> Notify([FromBody] NotifyRequest request)
    {
        await _notificationService.NotifyPatientAsync(request.Message, request.Channel);
        return Ok("Notification sent");
    }

    /// <summary>
    /// Test injection of Options Pattern and AppointmentService
    /// </summary>
    /// <returns>Clinic settings and appointment scheduling result</returns>
    [HttpGet("appointment-service")]
    public IActionResult TestAppointmentService()
    {
        // Call the service method
        _appointmentService.ScheduleAppointment();

        // Display injected settings for verification
        return Ok(new
        {
            ClinicName = _clinicOptions.ClinicName,
            MaxDailyAppointments = _clinicOptions.MaxDailyAppointments,
            AppointmentDurationMinutes = _clinicOptions.AppointmentDuration.TotalMinutes,
            Message = "AppointmentService was called successfully (check console for output)"
        });
    }

    /// <summary>
    /// Test appointment scheduling (ScheduleAsync)
    /// </summary>
    /// <param name="patientName">Patient name</param>
    /// <param name="dateTime">Appointment date and time</param>
    /// <returns>Scheduling result</returns>
    [HttpPost("schedule")]
    public async Task<IActionResult> Schedule(
        [FromBody] ScheduleAppointmentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.PatientName))
        {
            return BadRequest("Patient name is required");
        }

        var appointment = new Appointment(request.PatientName, request.DateTime);

        await _appointmentService.ScheduleAsync(appointment);

        return Ok(new
        {
            Message = "Appointment scheduled successfully",
            Patient = request.PatientName,
            DateTime = request.DateTime
        });
    }

}

/// <summary>
/// Request model for sending notification
/// </summary>
public class SendNotificationRequest
{
    /// <summary>
    /// Message text (Example: "Appointment reminder for tomorrow at 10 AM")
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Recipient (email address or mobile number)
    /// </summary>
    public string Recipient { get; set; } = string.Empty;
}

public class NotifyRequest
{
    public string Message { get; set; } = string.Empty;
    public string Channel { get; set; } = "email";
}

public class ScheduleAppointmentRequest
{
    public string PatientName { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
}