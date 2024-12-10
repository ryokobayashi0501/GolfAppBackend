using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GolfAppBackend.Models;
using System.Threading.Tasks;
using System.Linq;
using GolfAppBackend.Models.DTOs;
using WebApi_test.Models;

namespace GolfAppBackend.Controllers
{
    [Route("api/users/{userId}/rounds/{roundId}/holes")]
    [ApiController]
    public class RoundHolesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public RoundHolesController(ApiDbContext context)
        {
            _context = context;
        }

        // POST: api/users/{userId}/rounds/{roundId}/holes/{holeId}
        [HttpPost("{holeId}")]
        public async Task<ActionResult<RoundHole>> PostRoundHole(long userId, long roundId, long holeId, [FromBody] RoundHoleDto roundHoleDto)
        {
            // ラウンドが存在し、ユーザーに属しているか確認
            var round = await _context.Rounds.FirstOrDefaultAsync(r => r.roundId == roundId && r.userId == userId);
            if (round == null)
            {
                return BadRequest("Invalid roundId or the round does not belong to the specified user.");
            }

            // ホールが存在するか確認
            var hole = await _context.Holes.FirstOrDefaultAsync(h => h.holeId == holeId && h.courseId == round.courseId);
            if (hole == null)
            {
                return BadRequest("Invalid holeId or the hole does not belong to the specified course.");
            }

            // RoundHoleが既に存在する場合は新しく追加しない
            var existingRoundHole = await _context.RoundHoles.FirstOrDefaultAsync(rh => rh.roundId == roundId && rh.holeId == holeId);
            if (existingRoundHole != null)
            {
                return Conflict("A RoundHole for this round and hole already exists.");
            }

            // 新しい RoundHole オブジェクトを作成
            var roundHole = new RoundHole
            {
                roundId = roundId,
                holeId = holeId,
                stroke = roundHoleDto.stroke,
                putts = roundHoleDto.putts,
                weatherConditions = roundHoleDto.weatherConditions,
                penaltyStrokes = null,         // 初期状態で null
                bunkerShotsCount = null,       // 初期状態で null
                bunkerRecovery = null,         // 初期状態で null
                scrambleAttempted = null,      // 初期状態で null
                scrambleSuccess = null,        // 初期状態で null
                fairwayHit = null,             // 初期状態で null
                greenInRegulation = null       // 初期状態で null
            };

            _context.RoundHoles.Add(roundHole);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoundHole), new { userId, roundId, holeId }, roundHole);
        }

        // GET: api/users/{userId}/rounds/{roundId}/holes/{holeId}
        [HttpGet("{holeId}")]
        public async Task<ActionResult<RoundHole>> GetRoundHole(long userId, long roundId, long holeId)
        {
            var roundHole = await _context.RoundHoles
                .FirstOrDefaultAsync(rh => rh.roundId == roundId && rh.holeId == holeId);

            if (roundHole == null)
            {
                return NotFound();
            }

            return roundHole;
        }

        // GET: api/users/{userId}/rounds/holes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoundHole>>> GetAllRoundHolesForUser(long userId)
        {
            // ユーザーが所有するラウンドに関連するすべてのRoundHoleを取得
            var roundHoles = await _context.RoundHoles
                .Where(rh => rh.Round.userId == userId)
                .ToListAsync();

            if (roundHoles == null || !roundHoles.Any())
            {
                return NotFound("No RoundHoles found for the specified user.");
            }

            return Ok(roundHoles);
        }



        // PUT: api/users/{userId}/rounds/{roundId}/holes/{holeId}
        [HttpPut("{holeId}")]
        public async Task<IActionResult> UpdateRoundHole(long userId, long roundId, long holeId, [FromBody] RoundHoleDto roundHoleDto)
        {
            var roundHole = await _context.RoundHoles
                .FirstOrDefaultAsync(rh => rh.roundId == roundId && rh.holeId == holeId && rh.Round.userId == userId);

            if (roundHole == null)
            {
                // RoundHoleが存在しない場合、新規作成
                var newRoundHole = new RoundHole
                {
                    roundId = roundId,
                    holeId = holeId,
                    stroke = roundHoleDto.stroke,
                    putts = roundHoleDto.putts,
                    weatherConditions = roundHoleDto.weatherConditions
                    // 必要なフィールドをここに追加
                };

                _context.RoundHoles.Add(newRoundHole);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(UpdateRoundHole), new { userId, roundId, holeId }, newRoundHole);
            }

            // 更新処理
            roundHole.stroke = roundHoleDto.stroke;
            roundHole.putts = roundHoleDto.putts;
            roundHole.penaltyStrokes = roundHoleDto.penaltyStrokes;
            roundHole.weatherConditions = roundHoleDto.weatherConditions;
            roundHole.bunkerShotsCount = roundHoleDto.bunkerShotsCount;
            roundHole.bunkerRecovery = roundHoleDto.bunkerRecovery;
            roundHole.scrambleAttempted = roundHoleDto.scrambleAttempted;
            roundHole.scrambleSuccess = roundHoleDto.scrambleSuccess;
            roundHole.fairwayHit = roundHoleDto.fairwayHit;
            roundHole.greenInRegulation = roundHoleDto.greenInRegulation;

            _context.Entry(roundHole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.RoundHoles.Any(rh => rh.roundId == roundId && rh.holeId == holeId && rh.Round.userId == userId))
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
    }
}
