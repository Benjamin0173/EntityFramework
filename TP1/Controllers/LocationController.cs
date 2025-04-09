using Microsoft.AspNetCore.Mvc;
using TP1.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data;
using TP1.DTOs.LocationDTOs;

namespace TP1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly AppDbContext _context;

        public LocationController(ILogger<LocationController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: /location
        [HttpGet]
        public async Task<IEnumerable<LocationDTO>> Get()
        {
            try {
                return await _context.Locations
                .Select(l => new LocationDTO
                {
                    Id = l.Id,
                    Name = l.Name,
                    Address = l.Address,
                    City = l.City,
                    Country = l.Country,
                    Capacity = l.Capacity
                })
                .ToListAsync();
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération de la localisation.");
                throw;
            }
            
        }

        // GET: /location/1
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDTO>> GetById(int id)
        {
            try {
                var l = await _context.Locations.FindAsync(id);
                    if (l == null) return NotFound();

                    return new LocationDTO
                    {
                        Id = l.Id,
                        Name = l.Name,
                        Address = l.Address,
                        City = l.City,
                        Country = l.Country,
                        Capacity = l.Capacity
                    };
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération de la localisation.");
                throw;
            }
            
        }

        // POST: /location
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LocationDTO dto)
        {
            try {
                var newLocation = new Location
                {
                    Name = dto.Name,
                    Address = dto.Address,
                    City = dto.City,
                    Country = dto.Country,
                    Capacity = dto.Capacity
                };

                await _context.Locations.AddAsync(newLocation);
                await _context.SaveChangesAsync();

                dto.Id = newLocation.Id;
                return CreatedAtAction(nameof(GetById), new { id = newLocation.Id }, dto);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création de la localisation.");
                throw;
            }
            
        }

        // PUT: /location/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LocationDTO dto)
        {
            try {
                var location = await _context.Locations.FindAsync(id);
                if (location == null) return NotFound();

                location.Name = dto.Name;
                location.Address = dto.Address;
                location.City = dto.City;
                location.Country = dto.Country;
                location.Capacity = dto.Capacity;

                await _context.SaveChangesAsync();
                return NoContent();
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la modification de la localisation.");
                throw;
            }
            
        }

        // DELETE: /location/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try{
                var location = _context.Locations.FirstOrDefault(l => l.Id == id);
                if (location == null)
                {
                    return NotFound();
                }

                _context.Remove(location);
                return NoContent();
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppresion de la localisation.");
                throw;
            }
        }
    }
}
