using MedicalApp.Application;
using MedicalApp.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ScheduleController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("addOrEdit")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> AddOrEditSchedule([FromBody] ScheduleRequest request)
    {
        var schedule = await _context.Schedules
            .FirstOrDefaultAsync(s => s.DoctorId == request.DoctorId && s.AvailableTime == request.AvailableTime);

        if (schedule == null)
        {
            schedule = new Schedule
            {
                DoctorId = request.DoctorId,
                AvailableTime = request.AvailableTime
            };
            _context.Schedules.Add(schedule);
        }
        else
        {
            schedule.AvailableTime = request.AvailableTime;
        }

        await _context.SaveChangesAsync();
        return Ok(new { Success = true });
    }
}

public class ScheduleRequest
{
    public int DoctorId { get; set; }
    public DateTime AvailableTime { get; set; }
}
