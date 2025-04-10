using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP1.Controllers;
using TP1.Models;
using TP1.DTOs.ParticipantDTOs;
using Data; // Assuming AppDbContext is in the TP1.Data namespace
using Xunit;
using System.Linq;
using System.Threading.Tasks;

public class ParticipantControllerTests
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;

    public ParticipantControllerTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    private AppDbContext GetContext() => new AppDbContext(_dbContextOptions);

    private ParticipantController GetController()
    {
        var context = GetContext();
        context.Locations.RemoveRange(context.Locations);
        context.SaveChanges();
        var logger = new Microsoft.Extensions.Logging.LoggerFactory().CreateLogger<ParticipantController>();
        return new ParticipantController(logger, context);
    }

    // Test pour récupérer tous les participants
    [Fact]
    public async Task Get_ReturnsAllParticipants()
    {
        // Arrange
        using var context = GetContext();
        context.Participants.Add(new Participant { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" });
        context.Participants.Add(new Participant { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" });
        await context.SaveChangesAsync();

        var controller = GetController();

        // Act
        var result = await controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ParticipantDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    // Test pour ajouter un participant
    [Fact]
    public async Task Create_ReturnsCreatedParticipant()
    {
        // Arrange
        var controller = GetController();
        var dto = new ParticipantDTO
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        // Act
        var result = await controller.Create(dto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<ParticipantDTO>(createdAtActionResult.Value);
        Assert.Equal(dto.FirstName, returnValue.FirstName);
        Assert.Equal(dto.LastName, returnValue.LastName);
    }
}