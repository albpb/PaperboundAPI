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
    public class LlibresController : ControllerBase
    {
        private readonly PaperboundContext _context;

        public LlibresController(PaperboundContext context)
        {
            _context = context;
        }

        // GET: api/Llibres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Llibre>>> GetLlibres()
        {
            return await _context.Llibres.ToListAsync();
        }

        //// GET: api/Llibres/Ficción
        [HttpGet("{genreName}")]
        public async Task<ActionResult<IEnumerable<Llibre>>> GetLlibres(string genreName)
        {
            Genere genere = await _context.Generes.FirstOrDefaultAsync(x => x.NomGenere == genreName);

            var llibre = await _context.Llibres.Where(x => x.IdGenere == genere.IdGenere).ToListAsync();

            if (llibre == null)
            {
                return NotFound();
            }

            return llibre;
        }
        //// GET: api/Llibres/NombredelLibro
        [HttpGet("getByBookName")]
        public async Task<ActionResult<IEnumerable<Llibre>>> GetByTitol(string genreName, string bookName)
        {
            Genere genere = await _context.Generes.FirstOrDefaultAsync(x => x.NomGenere == genreName);

            List<Llibre> llibre = await _context.Llibres.Where(x => x.IdGenere == genere.IdGenere).ToListAsync();

            var llibrefinal = llibre.Where(x => x.Titol.ToUpper().Contains(bookName.ToUpper())).ToList();

            if (llibrefinal == null)
            {
                return NotFound();
            }

            return llibrefinal;
        }

        // GET: api/Llibres/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Llibre>> GetLlibre(int id)
        {
            var llibre = await _context.Llibres.FindAsync(id);

            if (llibre == null)
            {
                return NotFound();
            }

            return llibre;
        }

        // PUT: api/Llibres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLlibre(int id, Llibre llibre)
        {
            if (id != llibre.IdLlibre)
            {
                return BadRequest();
            }

            _context.Entry(llibre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LlibreExists(id))
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

        // POST: api/Llibres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Llibre>> PostLlibre(Llibre llibre)
        {
            _context.Llibres.Add(llibre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLlibre", new { id = llibre.IdLlibre }, llibre);
        }

        // DELETE: api/Llibres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLlibre(int id)
        {
            var llibre = await _context.Llibres.FindAsync(id);
            if (llibre == null)
            {
                return NotFound();
            }

            _context.Llibres.Remove(llibre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LlibreExists(int id)
        {
            return _context.Llibres.Any(e => e.IdLlibre == id);
        }
    }
}
