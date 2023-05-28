using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaperboundAPI.Models;
using PaperboundAPI.Resources;

namespace PaperboundAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarisController : ControllerBase
    {
        private readonly PaperboundContext _context;

        public UsuarisController(PaperboundContext context)
        {
            _context = context;
        }

        // GET: api/Usuaris
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuari>>> GetUsuaris()
        {
            return await _context.Usuaris.ToListAsync();
        }

        // GET: api/Usuaris/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuari>> GetUsuari(int id)
        {
            var usuari = await _context.Usuaris.FindAsync(id);

            if (usuari == null)
            {
                return NotFound();
            }

            return usuari;
        }

        // PUT: api/Usuaris/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuari(int id, Usuari usuari)
        {
            if (id != usuari.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(usuari).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariExists(id))
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

        // POST: api/Usuaris
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuari>> PostUsuari(Usuari usuari)
        {
            _context.Usuaris.Add(usuari);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuari", new { id = usuari.IdUser }, usuari);
        }
        [HttpPost("Login")]
        public async Task<bool> Login(LoginResource login)
        {
            List<Usuari> usuariComparar = _context.Usuaris.Where(x => x.Login == login.User).ToList();
            bool esCorrecto = false;

            for (int i = 0; i < usuariComparar.Count && !esCorrecto; i++)
            {
                Usuari temp = usuariComparar[i];

                using (SHA256 hash = SHA256.Create())
                {
                    byte[] hashedBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(login.Password + temp.Salt));
                    string password = BitConverter.ToString(hashedBytes);

                    if (password == temp.Password)
                    {
                        esCorrecto = true;
                    }
                }
            }

            return esCorrecto;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<Usuari>> Register(LoginResource login)
        {
            string saltHashed;
            string contrasenya;

            Random random = new Random();

            int saltBF = random.Next();

            using (SHA256 hash = SHA256.Create())
            {
                byte[] hashedBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(saltBF.ToString()));
                saltHashed = BitConverter.ToString(hashedBytes);

                byte[] hashedBytes2 = hash.ComputeHash(Encoding.UTF8.GetBytes(login.Password + saltHashed));
                contrasenya = BitConverter.ToString(hashedBytes2);

            }
            Usuari usuari = new Usuari
            {
                Login = login.User,
                Password = contrasenya,
                Salt = saltHashed,
            };

            _context.Usuaris.Add(usuari);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuari", new { id = usuari.IdUser }, usuari);
        }

        // DELETE: api/Usuaris/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuari(int id)
        {
            var usuari = await _context.Usuaris.FindAsync(id);
            if (usuari == null)
            {
                return NotFound();
            }

            _context.Usuaris.Remove(usuari);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariExists(int id)
        {
            return _context.Usuaris.Any(e => e.IdUser == id);
        }
    }
}
