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
    public class PuntsRecollidaController : ControllerBase
    {
        private readonly PaperboundContext _context;

        public PuntsRecollidaController(PaperboundContext context)
        {
            _context = context;
        }

        // GET: api/PuntsRecollida
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuntRecollida>>> GetPuntsRecollida()
        {
            return await _context.PuntsRecollida.ToListAsync();
        }

        // GET: api/PuntsRecollida/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PuntRecollida>> GetPuntRecollida(int id)
        {
            var puntRecollida = await _context.PuntsRecollida.FindAsync(id);

            if (puntRecollida == null)
            {
                return NotFound();
            }

            return puntRecollida;
        }

        // PUT: api/PuntsRecollida/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuntRecollida(int id, PuntRecollida puntRecollida)
        {
            if (id != puntRecollida.IdPuntRecollida)
            {
                return BadRequest();
            }

            _context.Entry(puntRecollida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuntRecollidaExists(id))
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

        // POST: api/PuntsRecollida
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PuntRecollida>> PostPuntRecollida(PuntRecollida puntRecollida)
        {
            _context.PuntsRecollida.Add(puntRecollida);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPuntRecollida", new { id = puntRecollida.IdPuntRecollida }, puntRecollida);
        }

        // DELETE: api/PuntsRecollida/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePuntRecollida(int id)
        {
            var puntRecollida = await _context.PuntsRecollida.FindAsync(id);
            if (puntRecollida == null)
            {
                return NotFound();
            }

            _context.PuntsRecollida.Remove(puntRecollida);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PuntRecollidaExists(int id)
        {
            return _context.PuntsRecollida.Any(e => e.IdPuntRecollida == id);
        }
    }
}
