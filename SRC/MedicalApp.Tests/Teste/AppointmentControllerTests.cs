using MedicalApp.Application;
using MedicalApp.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

public class AppointmentControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly AppointmentController _appointmentController;

    public AppointmentControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "AppointmentTestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _context.Appointments.Add(new Appointment { Id = 1, DoctorId = 1, PatientId = "patient1", AppointmentTime = DateTime.Now });
        _context.SaveChanges();

        _appointmentController = new AppointmentController(_context);
    }

    [Fact]
    public async Task AcceptOrDeclineAppointment_ReturnsOkResult_WhenAppointmentIsAccepted()
    {
        // Arrange
        var decisionRequest = new AppointmentDecisionRequest { AppointmentId = 1, Accept = true };

        // Act
        var result = await _appointmentController.AcceptOrDeclineAppointment(decisionRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
}
