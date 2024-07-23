using MedicalApp.Application;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class DoctorSearchController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DoctorSearchController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("search")]
    public IActionResult SearchDoctors(string specialty, string location)
    {
        var doctors = _context.Doctors
            .Where(d => d.Specialty == specialty && d.Location == location)
            .ToList();

        return Ok(doctors);
    }
}
