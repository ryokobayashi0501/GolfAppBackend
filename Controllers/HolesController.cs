using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GolfAppBackend.Models;
using WebApi_test.Models;

namespace GolfAppBackend.Controllers
{
    [Route("api/Courses/{courseId}/Holes")]
    [ApiController]
    public class HolesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public HolesController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses/{courseId}/Holes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hole>>> GetHoles(long courseId)
        {
            // コースが存在するか確認
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return NotFound("指定されたコースが見つかりません。");
            }

            // 指定されたコースに関連するホールを取得
            var holes = await _context.Holes
                .Where(h => h.courseId == courseId)
                .ToListAsync();

            return holes;
        }

        // GET: api/Courses/{courseId}/Holes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Hole>> GetHoleById(long courseId, long id)
        {
            // コースが存在するか確認
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return NotFound("指定されたコースが見つかりません。");
            }

            // 指定されたホールを取得
            var hole = await _context.Holes
                .FirstOrDefaultAsync(h => h.holeId == id && h.courseId == courseId);

            if (hole == null)
            {
                return NotFound("指定されたホールが見つかりません。");
            }

            return hole;
        }

        // HolesController.cs の POST メソッド
        [HttpPost]
        public async Task<ActionResult<Hole>> PostHole(long courseId, Hole hole)
        {
            // コースが存在するか確認
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return NotFound("指定されたコースが見つかりません。");
            }

            // ホールのCourseIdを設定
            hole.courseId = courseId;

            _context.Holes.Add(hole);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHoleById), new { courseId = courseId, id = hole.holeId }, hole);
        }


        // PUT: api/Courses/{courseId}/Holes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHole(long courseId, long id, Hole hole)
        {
            if (id != hole.holeId)
            {
                return BadRequest("ホールIDが一致しません。");
            }

            // コースが存在するか確認
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return NotFound("指定されたコースが見つかりません。");
            }

            // ホールのCourseIdを設定
            hole.courseId = courseId;

            _context.Entry(hole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoleExists(id))
                {
                    return NotFound("指定されたホールが見つかりません。");
                }
                else
                {
                    //this will be a problem, handle error in a badrequest http format, there is no exception catcher on the front end
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Courses/{courseId}/Holes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHole(long courseId, long id)
        {
            // コースが存在するか確認
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return NotFound("指定されたコースが見つかりません。");
            }

            var hole = await _context.Holes
                .FirstOrDefaultAsync(h => h.holeId == id && h.courseId == courseId);

            if (hole == null)
            {
                return NotFound("指定されたホールが見つかりません。");
            }

            _context.Holes.Remove(hole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HoleExists(long id)
        {
            return _context.Holes.Any(e => e.holeId == id);
        }
    }
}
