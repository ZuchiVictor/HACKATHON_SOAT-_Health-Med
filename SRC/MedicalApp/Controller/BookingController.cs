using MedicalApp.Application;
using MedicalApp.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BookingController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("book")]
    public async Task<IActionResult> BookAppointment([FromBody] BookingRequest request)
    {
        var appointment = new Appointment
        {
            DoctorId = request.DoctorId,
            AppointmentTime = request.AppointmentTime,
            PatientId = "test-patient" // Simule um ID de paciente
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Appointment booked successfully" });
    }
}
