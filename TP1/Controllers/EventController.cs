using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP1.Models;
using TP1.DTOs.EventDTOs;
using TP1.DTOs.ParticipantDTOs;
using Data;

namespace TP1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly AppDbContext _context;

        public EventController(ILogger<EventController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<EventDTO>> Get()
        {
            try
            {
                return await _context.Events
                    .Select(e => new EventDTO
                    {
                        Id = e.Id,
                        Title = e.Title,
                        Description = e.Description,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        Status = e.Status,
                        Category = e.Category,
                        LocationId = e.LocationId
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des événements.");
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> GetById(int id)
        {
            try
            {
                var e = await _context.Events.FindAsync(id);
                if (e == null) return NotFound();

                return new EventDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Status = e.Status,
                    Category = e.Category,
                    LocationId = e.LocationId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la récupération de l'événement avec ID {id}.");
                throw;
            }
        }

        [HttpGet("{id}/participants")]
        public async Task<ActionResult<List<ParticipantDTO>>> GetParticipantsByEventId(int id)
        {
            try
            {
                // Rechercher l'événement par ID
                var eventParticipants = await _context.EventParticipants
                    .Where(ep => ep.EventId == id)
                    .Include(ep => ep.Participant)  // Charger les participants associés
                    .Select(ep => ep.Participant)  // Sélectionner uniquement les participants
                    .ToListAsync();

                if (eventParticipants == null || !eventParticipants.Any())
                    return NotFound("Aucun participant trouvé pour cet événement.");

                // Projection des participants dans un ParticipantDTO
                var participantsDto = eventParticipants.Select(p => new ParticipantDTO
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    Company = p.Company,
                    JobTitle = p.JobTitle
                }).ToList();

                return participantsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la récupération des participants pour l'événement avec ID {id}.");
                return StatusCode(500, "Erreur interne du serveur.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventDTO dto)
        {
            try {
                var location = await _context.Locations.FindAsync(dto.LocationId);
                if (location == null)
                    return BadRequest("La localisation spécifiée n'existe pas.");

                var newEvent = new Event
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    Status = dto.Status,
                    Category = dto.Category,
                    LocationId = dto.LocationId
                };

                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = newEvent.Id }, dto);
            } catch (Exception ex) {
                _logger.LogError(ex, "Erreur lors de la création d'un nouvel événement.");
                return StatusCode(500, "Erreur interne du serveur.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EventDTO dto)
        {
            try {
                var eventItem = await _context.Events.FindAsync(id);
                if (eventItem == null) return NotFound();

                if (!await _context.Locations.AnyAsync(l => l.Id == dto.LocationId))
                    return BadRequest("La localisation spécifiée n'existe pas.");

                eventItem.Title = dto.Title;
                eventItem.Description = dto.Description;
                eventItem.StartDate = dto.StartDate;
                eventItem.EndDate = dto.EndDate;
                eventItem.Status = dto.Status;
                eventItem.Category = dto.Category;
                eventItem.LocationId = dto.LocationId;

                await _context.SaveChangesAsync();
                return NoContent();
            } catch (Exception ex) {
                _logger.LogError(ex, $"Erreur lors de la mise à jour de l'événement avec ID {id}.");
                return StatusCode(500, "Erreur interne du serveur.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
                return NotFound();

            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();

            return NoContent();
            } catch (Exception ex) {
                _logger.LogError(ex, $"Erreur lors de la suppression de l'événement avec ID {id}.");
                return StatusCode(500, "Erreur interne du serveur.");
            }
        }
    }
}
