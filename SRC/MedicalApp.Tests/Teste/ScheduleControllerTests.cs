using MedicalApp.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

public class ScheduleControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly ScheduleController _scheduleController;

    public ScheduleControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ScheduleTestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _scheduleController = new ScheduleController(_context);
    }

    [Fact]
    public async Task AddOrEditSchedule_ReturnsOkResult_WhenScheduleIsAdded()
    {
        // Arrange
        var scheduleRequest = new ScheduleRequest { DoctorId = 1, AvailableTime = DateTime.Now };

        // Act
        var result = await _scheduleController.AddOrEditSchedule(scheduleRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
}
