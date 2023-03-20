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
    public class ComandesController : ControllerBase
    {
        private readonly PaperboundContext _context;

        public ComandesController(PaperboundContext context)
        {
            _context = context;
        }

        // GET: api/Comandes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comanda>>> GetComandes()
        {
            return await _context.Comandes.ToListAsync();
        }

        // GET: api/Comandes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comanda>> GetComanda(int id)
        {
            var comanda = await _context.Comandes.FindAsync(id);

            if (comanda == null)
            {
                return NotFound();
            }

            return comanda;
        }

        // PUT: api/Comandes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComanda(int id, Comanda comanda)
        {
            if (id != comanda.Idcomanda)
            {
                return BadRequest();
            }

            _context.Entry(comanda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComandaExists(id))
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

        // POST: api/Comandes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comanda>> PostComanda(Comanda comanda)
        {
            _context.Comandes.Add(comanda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComanda", new { id = comanda.Idcomanda }, comanda);
        }

        // DELETE: api/Comandes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComanda(int id)
        {
            var comanda = await _context.Comandes.FindAsync(id);
            if (comanda == null)
            {
                return NotFound();
            }

            _context.Comandes.Remove(comanda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComandaExists(int id)
        {
            return _context.Comandes.Any(e => e.Idcomanda == id);
        }
    }
}
