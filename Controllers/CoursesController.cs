using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GolfAppBackend.Models;
using WebApi_test.Models;

namespace GolfAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CoursesController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courses>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Courses>> GetCourses(long id)
        {
            var courses = await _context.Courses.FindAsync(id);

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Courses>>> GetCoursesByUserId(long userId)
        {
            var courses = await (from round in _context.rounds
                                 join course in _context.Courses on round.courseId equals course.courseId
                                 where round.userId == userId
                                 select course).Distinct().ToListAsync();

            if (courses == null || !courses.Any())
            {
                return NotFound(); // No courses found for this user
            }

            return Ok(courses);
        }


        // POST: api/Courses/user/{userId}
        [HttpPost("user/{userId}")]
        public async Task<ActionResult<Courses>> PostCourseForUser(long userId, Courses course)
        {
            // コースをデータベースに追加
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            // コースが追加されたらそのコースに関連するラウンドを作成
            var newRound = new Rounds
            {
                userId = userId, // ユーザーIDを設定
                courseId = course.courseId, // 作成されたコースのIDを設定
                roundDate = DateTime.Now // 現在の日付を使用（必要に応じて変更）
            };

            // ラウンドデータをデータベースに追加
            _context.rounds.Add(newRound);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourses", new { id = course.courseId }, course);
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourses(long id, Courses courses)
        {
            if (id != courses.courseId)
            {
                return BadRequest();
            }

            _context.Entry(courses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // 関連するラウンドの更新 (必要に応じて更新が必要か検討)
                var rounds = await _context.rounds.Where(r => r.courseId == id).ToListAsync();
                foreach (var round in rounds)
                {
                    round.courseId = courses.courseId; // コース情報を更新する場合は反映
                    _context.Entry(round).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync(); // ラウンドの変更も保存
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursesExists(id))
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

        // CoursesExistsメソッドを追加して、特定のコースが存在するか確認
        private bool CoursesExists(long id)
        {
            return _context.Courses.Any(e => e.courseId == id);
        }


        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourses(long id)
        {
            var courses = await _context.Courses.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.Where(x => x.courseId == id).FirstOrDefaultAsync();

            // 関連するラウンドも削除
            //var rounds = await _context.rounds.Where(r => r.courseId == id).ToListAsync();
            //if (rounds.Any())
            //{
            //    _context.rounds.RemoveRange(rounds);
            //}

            _context.Courses.Remove(course);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());   
            }

            return NoContent();
        }

    }
}
