using Microsoft.AspNetCore.Mvc;
using TP1.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data;
using TP1.DTOs.EventParticipantDTOs;

namespace TP1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventParticipantController : ControllerBase
    {
        private readonly ILogger<EventParticipantController> _logger;
        private readonly AppDbContext _context;

        public EventParticipantController(ILogger<EventParticipantController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: /eventparticipant
        [HttpGet]
        public async Task<IEnumerable<EventParticipantDTO>> Get()
        {
            try { 
            return await _context.EventParticipants
                .Select(ep => new EventParticipantDTO
                {
                    EventId = ep.EventId,
                    ParticipantId = ep.ParticipantId,
                    RegistrationDate = ep.RegistrationDate,
                    AttendanceStatus = ep.AttendanceStatus
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des participants d'événements.");
                throw;
            }
        }

        // GET: /eventparticipant/1/1
        [HttpGet("{eventId}/{participantId}")]
        public async Task<ActionResult<EventParticipantDTO>> GetById(int eventId, int participantId)
        {
            try{
                var ep = await _context.EventParticipants
                    .FirstOrDefaultAsync(e => e.EventId == eventId && e.ParticipantId == participantId);

                if (ep == null) return NotFound();

                return new EventParticipantDTO
                {
                    EventId = ep.EventId,
                    ParticipantId = ep.ParticipantId,
                    RegistrationDate = ep.RegistrationDate,
                    AttendanceStatus = ep.AttendanceStatus
                };  
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération du participant d'événement.");
                throw;
            }
            
        }

        // POST: /eventparticipant
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventParticipantDTO dto)
        {
            try {
                var ep = new EventParticipant
                {
                    EventId = dto.EventId,
                    ParticipantId = dto.ParticipantId,
                    RegistrationDate = dto.RegistrationDate,
                    AttendanceStatus = dto.AttendanceStatus
                };

                _context.EventParticipants.Add(ep);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { eventId = ep.EventId, participantId = ep.ParticipantId }, dto);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du participant d'événement.");
                throw;
            }
            
        }

        // PUT: /eventparticipant/1/1
        [HttpPut("{eventId}/{participantId}")]
        public async Task<IActionResult> Update(int eventId, int participantId, [FromBody] EventParticipantDTO dto)
        {
            try {
                var ep = await _context.EventParticipants
                    .FirstOrDefaultAsync(e => e.EventId == eventId && e.ParticipantId == participantId);

                if (ep == null) return NotFound();

                ep.RegistrationDate = dto.RegistrationDate;
                ep.AttendanceStatus = dto.AttendanceStatus;

                await _context.SaveChangesAsync();
                return NoContent();
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la modification du participant d'événement.");
                throw;
            }
            
        }

        // DELETE: /eventparticipant/1/1
        [HttpDelete("{eventId}/{participantId}")]
        public async Task<IActionResult> Delete(int eventId, int participantId)
        {
            try {
                var eventParticipant = await _context.EventParticipants
                .FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.ParticipantId == participantId);

                if (eventParticipant == null)
                {
                    return NotFound();
                }

                _context.EventParticipants.Remove(eventParticipant);
                await _context.SaveChangesAsync();

                return NoContent();
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppresion du participant d'événement.");
                throw;
            }
            
        }
    }
}
