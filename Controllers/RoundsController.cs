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
    public class RoundsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public RoundsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Rounds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Round>>> GetRound()
        {
            return await _context.Round.ToListAsync();
        }

        // GET: api/Rounds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Round>> GetRound(long id)
        {
            var round = await _context.Round.FindAsync(id);

            if (round == null)
            {
                return NotFound();
            }

            return round;
        }

        // PUT: api/Rounds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRound(long id, Round round)
        {
            if (id != round.roundID)
            {
                return BadRequest();
            }

            _context.Entry(round).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoundExists(id))
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

        // POST: api/Rounds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Round>> PostRound(Round round)
        {
            _context.Round.Add(round);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRound", new { id = round.roundID }, round);
        }

        // DELETE: api/Rounds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRound(long id)
        {
            var round = await _context.Round.FindAsync(id);
            if (round == null)
            {
                return NotFound();
            }

            _context.Round.Remove(round);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoundExists(long id)
        {
            return _context.Round.Any(e => e.roundID == id);
        }
    }
}
