using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GolfAppBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolfAppBackend.Models.DTOs;
using WebApi_test.Models;
using GolfAppBackend.Models.Enums;

namespace GolfAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShotsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ShotsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/users/{userId}/rounds/{roundId}/holes/{holeId}/roundHoles/{roundHoleId}/shots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shot>>> GetShots(long userId, long roundId, long holeId, long roundHoleId)
        {
            var shots = await _context.Shots
                .Where(s => s.RoundHole.roundHoleId == roundHoleId)
                .ToListAsync();

            return shots;
        }

        // GET: api/users/{userId}/rounds/{roundId}/holes/{holeId}/roundHoles/{roundHoleId}/shots/{shotId}
        [HttpGet("{shotId}")]
        public async Task<ActionResult<Shot>> GetShot(long userId, long roundId, long holeId, long roundHoleId, long shotId)
        {
            var shot = await _context.Shots
                .FirstOrDefaultAsync(s => s.shotId == shotId && s.RoundHole.roundHoleId == roundHoleId);

            if (shot == null)
            {
                return NotFound();
            }

            return shot;
        }

        // POST: api/users/{userId}/rounds/{roundId}/holes/{holeId}/roundHoles/{roundHoleId}/shots
        // This method has been modified to handle bulk shot insertion.
        [HttpPost("bulk/{userId}/{roundId}/{holeId}/{roundHoleId}")]
        public async Task<ActionResult> BulkPostShots(long userId, long roundId, long holeId, long roundHoleId, [FromBody] List<ShotDto> shotDtos)
        {
            if (shotDtos == null || shotDtos.Count == 0)
            {
                return BadRequest("The shotDtos field is required and cannot be empty.");
            }

            var roundHole = await _context.RoundHoles
                .Include(rh => rh.Hole)
                .FirstOrDefaultAsync(rh => rh.roundHoleId == roundHoleId && rh.Round.roundId == roundId && rh.Round.userId == userId && rh.holeId == holeId);

            if (roundHole == null)
            {
                return BadRequest("RoundHole not found for the specified user, round, and hole.");
            }

            foreach (var shotDto in shotDtos)
            {
                var shotType = ParseShotType(shotDto.shotTypeName, shotDto.shotType);
                if (shotType == null)
                {
                    return BadRequest($"Invalid shotType value or name: {shotDto.shotType} of type {shotDto.shotTypeName}");
                }

                var shotResult = ParseShotResult(shotDto.shotResultName, shotDto.shotResult);
                if (shotResult == null && !string.IsNullOrEmpty(shotDto.shotResultName))
                {
                    return BadRequest($"Invalid shotResult value or name: {shotDto.shotResult} of type {shotDto.shotResultName}");
                }

                // Create Shot entity
                var shot = new Shot
                {
                    roundHoleId = roundHoleId,
                    shotNumber = shotDto.shotNumber,
                    distance = shotDto.distance,
                    remainingDistance = shotDto.remainingDistance,
                    clubUsed = shotDto.clubUsed,
                    ballDirection = shotDto.ballDirection,
                    shotType = shotType.Value,
                    ballHeight = shotDto.ballHeight,
                    lie = shotDto.lie,
                    shotResult = shotResult,
                    notes = shotDto.notes
                };

                _context.Shots.Add(shot);
            }

            await _context.SaveChangesAsync();

            return Ok("Shots have been successfully added.");
        }

        // Utility methods to parse enums dynamically
        private ShotType? ParseShotType(string shotTypeName, object shotTypeValue)
        {
            if (string.IsNullOrEmpty(shotTypeName) || shotTypeValue == null)
            {
                return null;
            }

            try
            {
                if (shotTypeName == nameof(ShotType))
                {
                    return (ShotType)Enum.Parse(typeof(ShotType), shotTypeValue.ToString());
                }
                if (shotTypeName == nameof(PuttType))
                {
                    return (ShotType)(PuttType)Enum.Parse(typeof(PuttType), shotTypeValue.ToString());
                }
            }
            catch
            {
                // If parsing fails, return null
            }

            return null;
        }

        private ShotResult? ParseShotResult(string shotResultName, object shotResultValue)
        {
            if (string.IsNullOrEmpty(shotResultName) || shotResultValue == null)
            {
                return null;
            }

            try
            {
                if (shotResultName == nameof(ShotResult))
                {
                    return (ShotResult)Enum.Parse(typeof(ShotResult), shotResultValue.ToString());
                }
                if (shotResultName == nameof(PuttResult))
                {
                    return (ShotResult?)(PuttResult)Enum.Parse(typeof(PuttResult), shotResultValue.ToString());
                }
            }
            catch
            {
                // If parsing fails, return null
            }

            return null;
        }

        // PUT: api/users/{userId}/rounds/{roundId}/holes/{holeId}/roundHoles/{roundHoleId}/shots/{shotId}
        [HttpPut("{shotId}")]
        public async Task<IActionResult> PutShot(long userId, long roundId, long holeId, long roundHoleId, long shotId, [FromBody] ShotDto shotDto)
        {
            if (shotId != shotDto.shotId)
            {
                return BadRequest("Shot ID mismatch.");
            }

            var existingShot = await _context.Shots
                .Include(s => s.RoundHole)
                .FirstOrDefaultAsync(s => s.shotId == shotId && s.RoundHole.roundHoleId == roundHoleId);

            if (existingShot == null)
            {
                return NotFound();
            }

            var shotType = ParseShotType(shotDto.shotTypeName, shotDto.shotType);
            if (shotType == null)
            {
                return BadRequest($"Invalid shotType value or name: {shotDto.shotType} of type {shotDto.shotTypeName}");
            }

            var shotResult = ParseShotResult(shotDto.shotResultName, shotDto.shotResult);
            if (shotResult == null && !string.IsNullOrEmpty(shotDto.shotResultName))
            {
                return BadRequest($"Invalid shotResult value or name: {shotDto.shotResult} of type {shotDto.shotResultName}");
            }

            existingShot.shotNumber = shotDto.shotNumber;
            existingShot.distance = shotDto.distance;
            existingShot.remainingDistance = shotDto.remainingDistance;
            existingShot.clubUsed = shotDto.clubUsed;
            existingShot.ballDirection = shotDto.ballDirection;
            existingShot.shotType = shotType.Value;
            existingShot.ballHeight = shotDto.ballHeight;
            existingShot.lie = shotDto.lie;
            existingShot.shotResult = shotResult;
            existingShot.notes = shotDto.notes;

            _context.Entry(existingShot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShotExists(shotId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/users/{userId}/rounds/{roundId}/holes/{holeId}/roundHoles/{roundHoleId}/shots/{shotId}
        [HttpDelete("{shotId}")]
        public async Task<IActionResult> DeleteShot(long userId, long roundId, long holeId, long roundHoleId, long shotId)
        {
            var shot = await _context.Shots
                .FirstOrDefaultAsync(s => s.shotId == shotId && s.RoundHole.roundHoleId == roundHoleId);

            if (shot == null)
            {
                return NotFound();
            }

            _context.Shots.Remove(shot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShotExists(long id)
        {
            return _context.Shots.Any(e => e.shotId == id);
        }
    }
}
