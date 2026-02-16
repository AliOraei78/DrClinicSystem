// Api/Options/ClinicOptions.cs
public class ClinicOptions
{
    public string ClinicName { get; set; } = "Default Clinic";
    public int MaxDailyAppointments { get; set; } = 50;
    public TimeSpan AppointmentDuration { get; set; } = TimeSpan.FromMinutes(15);
}