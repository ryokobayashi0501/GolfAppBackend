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
    public class ShotsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ShotsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Shots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shot>>> GetShot()
        {
            return await _context.Shot.ToListAsync();
        }

        // GET: api/Shots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shot>> GetShot(long id)
        {
            var shot = await _context.Shot.FindAsync(id);

            if (shot == null)
            {
                return NotFound();
            }

            return shot;
        }

        // PUT: api/Shots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShot(long id, Shot shot)
        {
            if (id != shot.shotID)
            {
                return BadRequest();
            }

            _context.Entry(shot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShotExists(id))
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

        // POST: api/Shots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shot>> PostShot(Shot shot)
        {
            _context.Shot.Add(shot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShot", new { id = shot.shotID }, shot);
        }

        // DELETE: api/Shots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShot(long id)
        {
            var shot = await _context.Shot.FindAsync(id);
            if (shot == null)
            {
                return NotFound();
            }

            _context.Shot.Remove(shot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShotExists(long id)
        {
            return _context.Shot.Any(e => e.shotID == id);
        }
    }
}
