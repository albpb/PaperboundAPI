using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PaperboundAPI.Models;
using PaperboundAPI.Resources;
using XSystem.Security.Cryptography;

namespace PaperboundAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrsController : ControllerBase
    {
        private readonly PaperboundContext _context;

        public QrsController(PaperboundContext context)
        {
            _context = context;
        }


        // POST: api/Llibres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostQr(QrResource qr)
        {
            PuntRecollida pr = _context.PuntsRecollida.Where(x => x.IdPuntRecollida == qr.IdPuntRecollida).FirstOrDefault();
            Llibre llibre = _context.Llibres.Where(x => x.IdLlibre == qr.IdLlibre).FirstOrDefault();

            if (pr == null || llibre == null)
            {
                return "";
            }
            Qr qr1 = new Qr
            {
                Code = GenerateUniqueCode(pr.Nombre + llibre.Titol),
                IdLlibre = llibre.IdLlibre,
                IdPuntRecollida = pr.IdPuntRecollida
            };

            _context.Qrs.Add(qr1);
            await _context.SaveChangesAsync();

            return qr1.Code;
        }
        
        string GenerateUniqueCode(string input)
        {
            input += DateTime.Now.ToString();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            SHA256Managed sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(inputBytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
