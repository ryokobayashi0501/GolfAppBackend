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
    public class RoundDetailsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public RoundDetailsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/RoundDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoundDetail>>> GetRoundDetail()
        {
            return await _context.RoundDetail.ToListAsync();
        }

        // GET: api/RoundDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoundDetail>> GetRoundDetail(long id)
        {
            var roundDetail = await _context.RoundDetail.FindAsync(id);

            if (roundDetail == null)
            {
                return NotFound();
            }

            return roundDetail;
        }

        // PUT: api/RoundDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoundDetail(long id, RoundDetail roundDetail)
        {
            if (id != roundDetail.roundDetailID)
            {
                return BadRequest();
            }

            _context.Entry(roundDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoundDetailExists(id))
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

        // POST: api/RoundDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoundDetail>> PostRoundDetail(RoundDetail roundDetail)
        {
            _context.RoundDetail.Add(roundDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoundDetail", new { id = roundDetail.roundDetailID }, roundDetail);
        }

        // DELETE: api/RoundDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoundDetail(long id)
        {
            var roundDetail = await _context.RoundDetail.FindAsync(id);
            if (roundDetail == null)
            {
                return NotFound();
            }

            _context.RoundDetail.Remove(roundDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoundDetailExists(long id)
        {
            return _context.RoundDetail.Any(e => e.roundDetailID == id);
        }
    }
}
