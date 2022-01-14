#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitatorBackend.Data;
using HospitatorBackend.Models;
using HospitatorBackend.Dtos;

namespace HospitatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtokolyController : ControllerBase
    {
        private readonly HospitatorDBContext _context;

        public ProtokolyController(HospitatorDBContext context)
        {
            _context = context;
        }

        // GET: api/Protokoly
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProtokolDto>>> GetProtokols()
        {
            return await _context.Protokoly.Select(p => new ProtokolDto()
            {
                Zakceptowane = p.Zakceptowane,
                DataWystawienia = p.DataWystawienia,
                DataZapoznania = p.DataZapoznania,
                Formulazprotokolus = p.Formulazprotokolus,
                Odwolanie = p.Odwolanie,
                NazwaKursu = p.Hospitacja.KursKodNavigation.Nazwa,
                KodKursu = p.Hospitacja.KursKodNavigation.Kod
            }).ToListAsync();
        }

        // GET: api/Protokoly/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("{id}")]
        public async Task<ActionResult<ProtokolDto>> GetProtokolById(int id)
        {
            return await _context.Protokoly
                .Include(p => p.Hospitacja.KursKodNavigation)
                .Where(p => p.Id == id)
                .Select(p => new ProtokolDto()
            {
                Zakceptowane = p.Zakceptowane,
                DataWystawienia = p.DataWystawienia,
                DataZapoznania = p.DataZapoznania,
                Formulazprotokolus = p.Formulazprotokolus,
                Odwolanie = p.Odwolanie,
                NazwaKursu = p.Hospitacja.KursKodNavigation.Nazwa,
                KodKursu = p.Hospitacja.KursKodNavigation.Kod
            }).FirstAsync();
        }

        // POST: api/Protokoly
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProtokolDto>> PostProtokol(FormularzProtokoluInputDto protokol)
        {
            var hospitacja = _context.Hospitacje.Find(protokol.HospitacjaId);

            if (hospitacja == null)
            {
                return BadRequest("hospitacja o podanym id nie istnieje");
            }

            var istiejaProtokoly = _context.Protokoly.Any(p => p.HospitacjaId == protokol.HospitacjaId);

            if (istiejaProtokoly)
            {
                return BadRequest("Dla danej hospitacji istnieje już wypełniony protokół");
            }

            Protokol p = new()
            {
                DataWystawienia = DateOnly.FromDateTime(DateTime.Now),
                HospitacjaId = hospitacja.Id,
            };

            _context.Protokoly.Add(p);
            _context.SaveChanges();

            Formulazprotokolu f = new()
            {
                ProtokolId = p.Id,
                LiczbaObecnych = protokol.LiczbaObecnych,
                OcenaKoncowa = protokol.OcenaKoncowa,
                Opuznienie = protokol.Opuznienie,
                PowodyNieprzystosowania = protokol.PowodyNieprzystosowania,
                Punktualnie = protokol.Punktualnie,
                SalaPrzystosowana = protokol.SalaPrzystosowana,
                SprawdzonoObecnosc = protokol.SprawdzonoObecnosc,
                TrescKursuZgodna = protokol.TrescKursuZgodna,
            };

            _context.FromularzeProtokolow.Add(f);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProtokolById), new { id = p.Id }, null );
        }
       
    }
}
