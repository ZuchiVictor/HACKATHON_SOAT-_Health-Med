using MedicalApp.Application;
using MedicalApp.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class DoctorSearchControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly DoctorSearchController _doctorSearchController;

    public DoctorSearchControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "DoctorSearchTestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _context.Doctors.AddRange(new List<Doctor>
        {
            new Doctor { Id = 1, Name = "Dr. Smith", Specialty = "Cardiology", Location = "NYC" },
            new Doctor { Id = 2, Name = "Dr. John", Specialty = "Neurology", Location = "LA" }
        });
        _context.SaveChanges();

        _doctorSearchController = new DoctorSearchController(_context);
    }

    [Fact]
    public void SearchDoctors_ReturnsOkResult_WithListOfDoctors()
    {
        // Act
        var result = _doctorSearchController.SearchDoctors("Cardiology", "NYC");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var doctors = Assert.IsType<List<Doctor>>(okResult.Value);
        Assert.Single(doctors);
        Assert.Equal("Dr. Smith", doctors.First().Name);
    }
}
