// Controllers/CoursesController.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GolfAppBackend.Models;
using WebApi_test.Models;
using System.Runtime.InteropServices;
using GolfAppBackend.Models.DTOs;

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
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _context.Courses.Include(c => c.Holes).ToListAsync();
            return courses;
        }


        // GET: api/Courses/name/{courseName}
        [HttpGet("name/{courseName}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourseByName(string courseName)
        {
            var courses = await _context.Courses
                .Where(c => EF.Functions.Like(c.courseName, $"%{courseName}%"))
                .Include(c => c.Holes)
                .ToListAsync();

            if (courses == null || courses.Count == 0)
            {
                return NotFound("No courses found with the specified name.");
            }

            return courses;
        }




        // POST: api/Rounds/user/{userId}/course/{courseId}
        [HttpPost("Rounds/user/{userId}/course/{courseId}")]
        public async Task<ActionResult<Round>> AddRoundForUser(long userId, long courseId)
        {
            var user = await _context.Users.FindAsync(userId);
            var course = await _context.Courses.FindAsync(courseId);

            if (user == null || course == null)
            {
                return NotFound("User or Course not found.");
            }

            var round = new Round
            {
                userId = userId,
                courseId = courseId,
                roundDate = DateTime.UtcNow
            };

            _context.Rounds.Add(round);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourseById), new { id = round.courseId }, round);
        }


        // GET: api/Courses/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByUserId(long userId)
        {
            // ユーザーが参加しているラウンドのコースを、Holesと一緒に取得
            var courses = await _context.Courses
                .Where(c => c.Rounds.Any(r => r.userId == userId))
                .Include(c => c.Holes)
                .ToListAsync();

            return courses;
        }


        [HttpPost("user/{userId}")]
        public async Task<ActionResult<Course>> PostCourse(long userId, [FromBody] Course course)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.userId == userId);
            if (user == null)
            {
                return NotFound("The specified user cannot be found.");
            }

            // コース名のチェック
            if (string.IsNullOrEmpty(course.courseName))
            {
                return BadRequest("CourseName is required.");
            }

            // Holesコレクションが正しいかチェック
            if (course.Holes == null || course.Holes.Count != 18)
            {
                return BadRequest("Exactly 18 holes are required.");
            }

            // 各ホールのHoleNumberが1から18になっているかチェック
            for (int i = 1; i <= 18; i++)
            {
                if (!course.Holes.Any(h => h.holeNumber == i))
                {
                    return BadRequest($"Hole number {i} is missing.");
                }
            }

            var round = new Round
            {
                userId = userId,
                Course = course,
                roundDate = DateTime.UtcNow
            };

            // コースをデータベースに追加
            _context.Courses.Add(course);
            _context.Rounds.Add(round);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourseById), new { id = course.courseId }, course);
        }



        // POST: api/Courses/user/{userId}/select
        [HttpPost("user/{userId}/select")]
        public async Task<ActionResult<RoundDTO>> SelectCourseForUser(long userId, [FromBody] long courseId)
        {
            var user = await _context.Users.FindAsync(userId);
            var course = await _context.Courses.FindAsync(courseId);

            if (user == null || course == null)
            {
                return NotFound("User or Course not found.");
            }

            var round = new Round
            {
                userId = userId,
                courseId = courseId,
                roundDate = DateTime.UtcNow // ラウンドが選択された日付
            };

            _context.Rounds.Add(round);
            await _context.SaveChangesAsync();

            var roundDto = new RoundDTO
            {
                roundId = round.roundId,
                courseId = round.courseId,
                roundDate = round.roundDate
            };

            return CreatedAtAction(nameof(GetCourseById), new { id = round.courseId }, roundDto);
        }




        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse([FromBody] Course course)
        {
            // コース名のチェック
            if (string.IsNullOrEmpty(course.courseName))
            {
                return BadRequest("CourseName is required.");
            }

            // Holesコレクションが正しいかチェック
            if (course.Holes == null || course.Holes.Count != 18)
            {
                return BadRequest("Exactly 18 holes are required.");
            }

            // 各ホールのHoleNumberが1から18になっているかチェック
            for (int i = 1; i <= 18; i++)
            {
                if (!course.Holes.Any(h => h.holeNumber == i))
                {
                    return BadRequest($"Hole number {i} is missing.");
                }
            }

            // コースをデータベースに追加
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourseById), new { id = course.courseId }, course);
        }


        [HttpPost("bulk")] //add courses at one time
        public async Task<ActionResult> PostMultipleCourses([FromBody] List<Course> courses)
        {
            foreach (var course in courses)
            {
                if (string.IsNullOrEmpty(course.courseName))
                {
                    return BadRequest("Each course must have a name.");
                }

                if (course.Holes == null || course.Holes.Count != 18)
                {
                    return BadRequest("Each course must have exactly 18 holes.");
                }

                for (int i = 1; i <= 18; i++)
                {
                    if (!course.Holes.Any(h => h.holeNumber == i))
                    {
                        return BadRequest($"Hole number {i} is missing for course {course.courseName}.");
                    }
                }

                _context.Courses.Add(course);
            }

            await _context.SaveChangesAsync();

            return Ok("Courses added successfully.");
        }




        // GET: api/Courses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseById(long id)
        {
            var course = await _context.Courses
                .Include(c => c.Holes)
                .FirstOrDefaultAsync(c => c.courseId == id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(long id, Course course)
        {
            if (id != course.courseId)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // DELETE: api/Courses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(long id)
        {
            var course = await _context.Courses
                .Include(c => c.Holes)
                .FirstOrDefaultAsync(c => c.courseId == id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(long id)
        {
            return _context.Courses.Any(e => e.courseId == id);
        }
    }


}
