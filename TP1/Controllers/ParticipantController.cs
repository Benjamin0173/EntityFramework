using Microsoft.AspNetCore.Mvc;
using TP1.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data;
using TP1.DTOs.ParticipantDTOs;
using TP1.DTOs.EventDTOs;


namespace TP1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly ILogger<ParticipantController> _logger;
        private readonly AppDbContext _context;

        public ParticipantController(ILogger<ParticipantController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: /participant
        [HttpGet]
        public async Task<IEnumerable<ParticipantDTO>> Get()
        {
            try {
                return await _context.Participants
                .Select(p => new ParticipantDTO
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    Company = p.Company,
                    JobTitle = p.JobTitle
                })
                .ToListAsync();
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des participants.");
                throw;
            }
            
        }

        // GET: /participant/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipantDTO>> GetById(int id)
        {
            try{
                var p = await _context.Participants.FindAsync(id);
                if (p == null) return NotFound();

                return new ParticipantDTO
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    Company = p.Company,
                    JobTitle = p.JobTitle
                };
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération du participant.");
                throw;
            }
        }

    [HttpGet("{id}/events")]
    public async Task<ActionResult<List<EventDTO>>> GetEventsByParticipantId(int id)
    {
        try
        {
            // Rechercher les événements associés au participant
            var eventParticipants = await _context.EventParticipants
                .Where(ep => ep.ParticipantId == id)
                .Include(ep => ep.Event)  // Charger les événements associés
                .Select(ep => ep.Event)   // Sélectionner uniquement les événements
                .ToListAsync();

            if (eventParticipants == null || !eventParticipants.Any())
                return NotFound("Aucun événement trouvé pour ce participant.");

            // Projection des événements dans un EventDTO
            var eventsDto = eventParticipants.Select(e => new EventDTO
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Status = e.Status,
                Category = e.Category,
                LocationId = e.LocationId
            }).ToList();

            return eventsDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la récupération des événements pour le participant avec ID {id}.");
            return StatusCode(500, "Erreur interne du serveur.");
        }
    }



        // POST: /participant
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ParticipantDTO dto)
        {
            try {
                var newP = new Participant
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Company = dto.Company,
                    JobTitle = dto.JobTitle
                };

                _context.Participants.Add(newP);
                await _context.SaveChangesAsync();

                dto.Id = newP.Id;
                return CreatedAtAction(nameof(GetById), new { id = newP.Id }, dto);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la créations du participant.");
                throw;
            }
        }

        // PUT: /participant/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ParticipantDTO dto)
        {
            try {
                var participant = await _context.Participants.FindAsync(id);
                if (participant == null) return NotFound();

                participant.FirstName = dto.FirstName;
                participant.LastName = dto.LastName;
                participant.Email = dto.Email;
                participant.Company = dto.Company;
                participant.JobTitle = dto.JobTitle;

                await _context.SaveChangesAsync();
                return NoContent();
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la modification des participants.");
                throw;
            }
        }

        // DELETE: /participant/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try{
                var participant = await _context.Participants.FindAsync(id);
                if (participant == null)
                {
                    return NotFound();
                }

                _context.Participants.Remove(participant);
                await _context.SaveChangesAsync();

                return NoContent();
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppresion du participant.");
                throw;
            }
            
        }
    }
}
