// Core/Entities/Appointment.cs
namespace DrClinicSystem.Core.Entities;

public class Appointment
{
    public Guid Id { get; private set; }
    public string PatientName { get; private set; }
    public DateTime DateTime { get; private set; }

    public Appointment(string patientName, DateTime dateTime)
    {
        Id = Guid.NewGuid();
        PatientName = patientName ?? throw new ArgumentNullException(nameof(patientName));
        DateTime = dateTime;
    }
}