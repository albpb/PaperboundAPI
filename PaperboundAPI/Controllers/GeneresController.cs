using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaperboundAPI.Models;

namespace PaperboundAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneresController : ControllerBase
    {
        private readonly PaperboundContext _context;

        public GeneresController(PaperboundContext context)
        {
            _context = context;
        }

        // GET: api/Generes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genere>>> GetGeneres()
        {
            return await _context.Generes.ToListAsync();
        }

        // GET: api/Generes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genere>> GetGenere(int id)
        {
            var genere = await _context.Generes.FindAsync(id);

            if (genere == null)
            {
                return NotFound();
            }

            return genere;
        }

        // PUT: api/Generes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenere(int id, Genere genere)
        {
            if (id != genere.IdGenere)
            {
                return BadRequest();
            }

            _context.Entry(genere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenereExists(id))
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

        // POST: api/Generes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Genere>> PostGenere(Genere genere)
        {
            _context.Generes.Add(genere);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenere", new { id = genere.IdGenere }, genere);
        }

        // DELETE: api/Generes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenere(int id)
        {
            var genere = await _context.Generes.FindAsync(id);
            if (genere == null)
            {
                return NotFound();
            }

            _context.Generes.Remove(genere);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenereExists(int id)
        {
            return _context.Generes.Any(e => e.IdGenere == id);
        }
    }
}
