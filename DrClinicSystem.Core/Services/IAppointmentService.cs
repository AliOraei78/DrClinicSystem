using System.Threading.Tasks;
using DrClinicSystem.Core.Entities;

namespace DrClinicSystem.Core.Services;

public interface IAppointmentService
{
    Task ScheduleAsync(Appointment appointment);
}