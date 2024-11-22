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
        public async Task<ActionResult<IEnumerable<Round>>> Getrounds()
        {
            return await _context.Rounds.ToListAsync();
        }

        // GET: api/Rounds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Round>> GetRounds(long id)
        {
            var rounds = await _context.Rounds.FindAsync(id);

            if (rounds == null)
            {
                return NotFound();
            }

            return rounds;
        }

        // PUT: api/Rounds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRounds(long id, Round rounds)
        {
            if (id != rounds.roundId)
            {
                return BadRequest();
            }

            _context.Entry(rounds).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoundsExists(id))
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
        public async Task<ActionResult<Round>> PostRounds(Round rounds)
        {
            _context.Rounds.Add(rounds);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRounds", new { id = rounds.roundId }, rounds);
        }

        // DELETE: api/Rounds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRounds(long id)
        {
            var rounds = await _context.Rounds.FindAsync(id);
            if (rounds == null)
            {
                return NotFound();
            }

            _context.Rounds.Remove(rounds);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoundsExists(long id)
        {
            return _context.Rounds.Any(e => e.roundId == id);
        }
    }
}
