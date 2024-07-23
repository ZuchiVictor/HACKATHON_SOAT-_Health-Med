using MedicalApp.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AppointmentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("acceptOrDecline")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> AcceptOrDeclineAppointment([FromBody] AppointmentDecisionRequest request)
    {
        var appointment = await _context.Appointments.FindAsync(request.AppointmentId);

        if (appointment == null)
        {
            return NotFound("Appointment not found");
        }

        appointment.IsAccepted = request.Accept;
        await _context.SaveChangesAsync();
        return Ok(new { Success = true });
    }
}

public class AppointmentDecisionRequest
{
    public int AppointmentId { get; set; }
    public bool Accept { get; set; }
}
