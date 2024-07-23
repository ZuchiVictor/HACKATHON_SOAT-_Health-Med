using MedicalApp.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;
using MedicalApp.DAO;

public class BookingControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly BookingController _bookingController;

    public BookingControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "BookingTestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _bookingController = new BookingController(_context);
    }

    [Fact]
    public async Task BookAppointment_ReturnsOkResult_WhenBookingIsSuccessful()
    {
        // Arrange
        var bookingRequest = new BookingRequest { DoctorId = 1, AppointmentTime = DateTime.Now };

        // Act
        var result = await _bookingController.BookAppointment(bookingRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
}
