using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GolfAppBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolfAppBackend.Models.DTOs;
using WebApi_test.Models;

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

        // GET: api/users/{userId}/rounds/{roundId}/shots
        [HttpGet("users/{userId}/rounds/{roundId}/shots")]
        public async Task<ActionResult<IEnumerable<ShotDto>>> GetShotsByRoundId(long userId, long roundId)
        {
            var shots = await _context.Shots
                .Where(s => s.RoundHole.Round.roundId == roundId && s.RoundHole.Round.userId == userId)
                .ToListAsync();

            List<ShotDto> result = new List<ShotDto>();

            foreach (var shot in shots) 
            {
                ShotDto newDto = new ShotDto()
                {
                    ballDirection = shot.ballDirection,
                    ballHeight = shot.ballHeight,
                    clubUsed = shot.clubUsed,
                    distance = shot.distance,
                    lie = shot.lie,
                    notes = shot.notes,
                    remainingDistance = shot.remainingDistance,
                    shotId = shot.shotId,
                    shotNumber = shot.shotNumber,
                    shotResult = shot.shotResult ?? string.Empty,
                    shotResultName = shot.shotResult ?? string.Empty,
                    shotType = shot.shotType ?? string.Empty,
                    shotTypeName = shot.shotType ?? string.Empty
                };
                result.Add(newDto);

            }   

            if (shots == null || shots.Count == 0)
            {
                return NotFound("No shots found for the specified round.");
            }

            return Ok(result);
        }


        // POST: api/users/{userId}/rounds/{roundId}/holes/{holeId}/roundHoles/{roundHoleId}/shots
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
                var shot = new Shot
                {
                    roundHoleId = roundHoleId,
                    shotNumber = shotDto.shotNumber,
                    distance = shotDto.distance,
                    remainingDistance = shotDto.remainingDistance,
                    clubUsed = shotDto.clubUsed,
                    ballDirection = shotDto.ballDirection,
                    shotType = shotDto.shotType,
                    ballHeight = shotDto.ballHeight,
                    lie = shotDto.lie,
                    shotResult = shotDto.shotResult,
                    notes = shotDto.notes
                };

                _context.Shots.Add(shot);
            }

            await _context.SaveChangesAsync();  // 全てのショットを保存した後に RoundHole の情報を更新する

            // 全てのショットが保存された後に RoundHole を更新
            UpdateRoundHoleAfterAllShots(roundHole);

            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();

            return Ok("Shots have been successfully added.");
        }

        /// <summary>
        /// ショットの追加・更新後に RoundHole の関連フィールドを更新します。
        /// </summary>
        /// <param name="roundHole">更新対象の RoundHole</param>
        /// <param name="shotDto">Shot のデータ</param>
        /// <param name="shot">Shot エンティティ</param>
        private void UpdateRoundHoleAfterAllShots(RoundHole roundHole)
        {
            // 初期化
            roundHole.fairwayHit = null;
            roundHole.penaltyStrokes = 0;
            roundHole.greenInRegulation = false;
            roundHole.scrambleAttempted = null;
            roundHole.scrambleSuccess = null;
            roundHole.bunkerShotsCount = 0;
            roundHole.bunkerRecovery = null;

            var shots = _context.Shots.Where(s => s.RoundHole.roundHoleId == roundHole.roundHoleId).OrderBy(s => s.shotNumber).ToList();
            if (shots == null || shots.Count == 0) return;

            foreach (var shot in shots)
            {
                int? par = roundHole.Hole?.par;
                if (!par.HasValue)
                {
                    // パーが不明な場合は処理を中断
                    return;
                }

                // フェアウェイヒットの更新（ティーショットのみ）
                if (shot.shotNumber == 1 && shot.ballDirection == "Fairway")
                {
                    roundHole.fairwayHit = true;
                }

                // OB の場合、ペナルティストロークの追加
                if (shot.ballDirection == "LeftOB" || shot.ballDirection == "RightOB")
                {
                    roundHole.penaltyStrokes += 2;
                }
                else if (shot.ballDirection == "WaterHazardLeft" || shot.ballDirection == "WaterHazardRight" || shot.ballDirection == "WaterHazardFront")
                {
                    roundHole.penaltyStrokes += 1;
                }

                // パーオンの更新
                if ((roundHole.stroke - roundHole.putts) <= (par.Value - 2))
                {
                    roundHole.greenInRegulation = true;
                }
                else
                {
                    roundHole.greenInRegulation = false;
                    roundHole.scrambleAttempted = true;
                }

                // スクランブルの更新
                if (roundHole.scrambleAttempted.GetValueOrDefault() && roundHole.stroke <= par.Value)
                {
                    roundHole.scrambleSuccess = true;
                }
                else
                {
                    roundHole.scrambleSuccess = false;
                }

                // バンカーショットのカウント
                if (shot.ballDirection == "SandBunkerLeft" || shot.ballDirection == "SandBunkerRight")
                {
                    roundHole.bunkerShotsCount++;
                }

                // バンカーからのリカバリー判定
                if (shot.shotNumber >= 2 && shot.clubUsed != "Putter")
                {
                    var previousShot = shots.FirstOrDefault(s => s.shotNumber == shot.shotNumber - 1);

                    if (previousShot != null &&
                        (previousShot.ballDirection == "SandBunkerLeft" || previousShot.ballDirection == "SandBunkerRight"))
                    {
                        if (previousShot.remainingDistance <= 50)
                        {
                            if (shot.ballDirection == "Green" && (roundHole.putts ?? 0) == 1)
                            {
                                roundHole.bunkerRecovery = true;
                            }
                            else if (shot.shotResult == "Holed")
                            {
                                roundHole.bunkerRecovery = true;
                            }
                            else
                            {
                                roundHole.bunkerRecovery = false;
                            }
                        }
                        else
                        {
                            roundHole.bunkerRecovery = false;
                        }
                    }
                }
            }
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

            existingShot.shotNumber = shotDto.shotNumber;
            existingShot.distance = shotDto.distance;
            existingShot.remainingDistance = shotDto.remainingDistance;
            existingShot.clubUsed = shotDto.clubUsed;
            existingShot.ballDirection = shotDto.ballDirection;
            existingShot.shotType = shotDto.shotType;
            existingShot.ballHeight = shotDto.ballHeight;
            existingShot.lie = shotDto.lie;
            existingShot.shotResult = shotDto.shotResult;
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
