using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TP1.Controllers;
using TP1.Models;
using TP1.DTOs.LocationDTOs;
using Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

public class LocationControllerTests
{
    // [Fact]
    // public void Test_Addition_TwoPlusTwo_EqualsFour()
    // {
    //     // Arrange
    //     int a = 2;
    //     int b = 2;

    //     // Act
    //     int result = a + b;

    //     // Assert
    //     Assert.Equal(4, result);
    // }

    // [Fact]
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new AppDbContext(options);
    }

    private void ResetBdd()
    {
        var _context = GetInMemoryDbContext();
        _context.Locations.RemoveRange(_context.Locations);
        _context.SaveChanges();
    }

    [Fact]
    public async Task Get_ReturnsAllLocations()
    {
        ResetBdd();
        // Arrange
        var context = GetInMemoryDbContext();
        context.Locations.AddRange(
            new Location { Name = "Loc1", Address = "Adr1", City = "City1", Country = "FR", Capacity = 100 },
            new Location { Name = "Loc2", Address = "Adr2", City = "City2", Country = "FR", Capacity = 200 }
        );
        context.SaveChanges();

        var logger = new LoggerFactory().CreateLogger<LocationController>();
        var controller = new LocationController(logger, context);

        // Act
        var result = await controller.Get();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, l => l.Name == "Loc1");
    }

    [Fact]
    public async Task GetById_ReturnsLocation_WhenLocationExists()
    {
        ResetBdd();
        // Arrange
        var context = GetInMemoryDbContext();
        var location = new Location { Name = "Loc1", Address = "Adr1", City = "City1", Country = "FR", Capacity = 100 };
        context.Locations.Add(location);
        context.SaveChanges();

        var logger = new LoggerFactory().CreateLogger<LocationController>();
        var controller = new LocationController(logger, context);

        // Act
        var result = await controller.GetById(location.Id);

        // Assert
        var actionResult = Assert.IsType<ActionResult<LocationDTO>>(result);
        var locationDTO = Assert.IsType<LocationDTO>(actionResult.Value);
        Assert.Equal(location.Name, locationDTO.Name);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenLocationDoesNotExist()
    {
        ResetBdd();
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new LoggerFactory().CreateLogger<LocationController>();
        var controller = new LocationController(logger, context);

        // Act
        var result = await controller.GetById(999); // ID qui n'existe pas

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Create_CreatesLocation_ReturnsCreated()
    {
        ResetBdd();
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new LoggerFactory().CreateLogger<LocationController>();
        var controller = new LocationController(logger, context);

        var newLocation = new LocationDTO
        {
            Name = "Loc3",
            Address = "Adr3",
            City = "City3",
            Country = "FR",
            Capacity = 150
        };

        // Act
        var result = await controller.Create(newLocation);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdLocation = Assert.IsType<LocationDTO>(actionResult.Value);
        Assert.Equal(newLocation.Name, createdLocation.Name);
    }

    [Fact]
    public async Task Update_UpdatesLocation_ReturnsNoContent()
    {
        ResetBdd();
        // Arrange
        var context = GetInMemoryDbContext();
        var location = new Location { Name = "Loc1", Address = "Adr1", City = "City1", Country = "FR", Capacity = 100 };
        context.Locations.Add(location);
        context.SaveChanges();

        var updatedLocationDTO = new LocationDTO
        {
            Name = "UpdatedLoc",
            Address = "UpdatedAdr",
            City = "UpdatedCity",
            Country = "UpdatedCountry",
            Capacity = 250
        };

        var logger = new LoggerFactory().CreateLogger<LocationController>();
        var controller = new LocationController(logger, context);

        // Act
        var result = await controller.Update(location.Id, updatedLocationDTO);

        // Assert
        Assert.IsType<NoContentResult>(result);
        var updatedLocation = context.Locations.Find(location.Id);
        Assert.Equal(updatedLocationDTO.Name, updatedLocation.Name);
    }

    [Fact]
    public async Task Delete_RemovesLocation_ReturnsNoContent()
    {
        ResetBdd();
        // Arrange
        var context = GetInMemoryDbContext();
        var location = new Location { Name = "Loc1", Address = "Adr1", City = "City1", Country = "FR", Capacity = 100 };
        context.Locations.Add(location);
        context.SaveChanges();

        var logger = new LoggerFactory().CreateLogger<LocationController>();
        var controller = new LocationController(logger, context);

        // Act
        var result = await (controller.Delete(location.Id) as Task<IActionResult>);

        // Assert
        Assert.IsType<NoContentResult>(result);
        var deletedLocation = context.Locations.Find(location.Id);
        Assert.Null(deletedLocation);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenLocationDoesNotExist()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new LoggerFactory().CreateLogger<LocationController>();
        var controller = new LocationController(logger, context);

        // Act
        var result = await controller.Delete(999); // ID qui n'existe pas

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    
}
