using HospitatorBackend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HospitatorBackend.Dtos;
using HospitatorBackend.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcenyController : ControllerBase
    {
        private readonly HospitatorDBContext _context;

        public OcenyController(HospitatorDBContext context)
        {
            _context = context;
        }

        [HttpGet("{id_prowadzacego:int}")]
        public ActionResult<PrzegladOcenDto> GetOcenyNauczycielaById(int id_prowadzacego)
        {
            var query_nowe = from p in _context.Protokoly
                             where p.Hospitacja.Prowadzacy.Id == id_prowadzacego
                             && p.Zakceptowane == false
                             && p.Odwolanie == null
                             select new ProtokolDto()
                             {
                                 Id = p.Id,
                                 Zakceptowane = p.Zakceptowane,
                                 DataWystawienia = p.DataWystawienia,
                                 DataZapoznania = p.DataZapoznania,
                                 HospitacjaId = p.HospitacjaId,
                                 Formulazprotokolus = p.Formulazprotokolus,
                                 Odwolanie = p.Odwolanie,
                                 Kurs = p.Hospitacja.KursKodNavigation,
                             };

            var query_zakceptowane = from p in _context.Protokoly
                                     where p.Hospitacja.Prowadzacy.Id == id_prowadzacego
                                     && p.Zakceptowane == true
                                     select new ProtokolDto()
                                     {
                                         Id = p.Id,
                                         Zakceptowane = p.Zakceptowane,
                                         DataWystawienia = p.DataWystawienia,
                                         DataZapoznania = p.DataZapoznania,
                                         HospitacjaId = p.HospitacjaId,
                                         Formulazprotokolus = p.Formulazprotokolus,
                                         Odwolanie = p.Odwolanie
                                     };

            var query_zareklamowane = from p in _context.Protokoly
                                      where p.Hospitacja.Prowadzacy.Id == id_prowadzacego
                                      && p.Odwolanie != null
                                      select new ProtokolDto()
                                      {
                                          Id = p.Id,
                                          Zakceptowane = p.Zakceptowane,
                                          DataWystawienia = p.DataWystawienia,
                                          DataZapoznania = p.DataZapoznania,
                                          HospitacjaId = p.HospitacjaId,
                                          Formulazprotokolus = p.Formulazprotokolus,
                                          Odwolanie = p.Odwolanie
                                      };

            return Ok(new PrzegladOcenDto()
            {
                Nowe = query_nowe.ToList(),
                Zakceptowane = query_zakceptowane.ToList(),
                Zareklamowane = query_zareklamowane.ToList(),
            });
        }

        [HttpGet("{id_prowadzacego:int}/{id_protokolu:int}")]
        public ActionResult<ProtokolDto> GetOcena(int id_prowadzacego, int id_protokolu)
        {
            var p = _context.Protokoly
                .Include(p => p.Hospitacja.KursKodNavigation)
                .Include(p => p.Odwolanie)
                .Include(p => p.Formulazprotokolus)
                .FirstOrDefault(p => p.Id == id_protokolu);

            if (p == null)
            {
                return NotFound();
            }

            if (p.Hospitacja.ProwadzacyId != id_prowadzacego)
            {
                return BadRequest("Ta ocena nie nalerzy do ciebie");
            }

            return Ok(new ProtokolDto()
            {
                Zakceptowane = p.Zakceptowane,
                DataWystawienia = p.DataWystawienia,
                DataZapoznania = p.DataZapoznania,
                HospitacjaId = p.HospitacjaId,
                Formulazprotokolus = p.Formulazprotokolus,
                Odwolanie = p.Odwolanie,
                Kurs = p.Hospitacja.KursKodNavigation
            });
        }


        [HttpPost("Reklamuj")]
        public ActionResult<ReklamacjaDto> ZareklamujOcene(ReklamacjaDto r)
        {
            var protokol = _context.Protokoly.First(p => p.Id == r.ProtokolId && p.Hospitacja.Prowadzacy.Id == r.ProwadzacyId);

            if (protokol == null)
            {
                return BadRequest("Podany protokol nie istnieje lub prowadzacy nie jest jego podmiotem");
            }

            var istniejeRekalmacja = _context.Odwolania.Any(o => o.ProtokolId == r.ProtokolId);

            if (istniejeRekalmacja)
            {
                return BadRequest("Reklamacja już istnieje");
            }

            Odwolanie rekalamcja = new()
            {
                DataOdwolania = DateOnly.FromDateTime(DateTime.Now),
                ProtokolId = r.ProtokolId,
                ProwadzacyId = r.ProwadzacyId,
                Status = "oczekujaca", // hardkodowane string, do poprawy jak będzie czas
                Uzasadnienie = r.Uzasadnienie
            };

            _context.Odwolania.Add(rekalamcja);
            _context.SaveChanges();

            // powinno być 201 ale createdAt wymaga lokalizacji utworzenogo obiektu
            return Ok(r);
        }

        [HttpPut("{id_nauczyciela:int}/{id_protokolu:int}")]
        public async Task<ActionResult<ProtokolDto>> ZakceptujOcene(int id_nauczyciela, int id_protokolu)
        {
            var protokol = _context.Protokoly
                .Where(p => p.Hospitacja.Prowadzacy.Id == id_nauczyciela && p.Odwolanie == null)
                .First(p => p.Id == id_protokolu);

            if (protokol == null)
            {
                return BadRequest();
            }

            protokol.Zakceptowane = true;

            _context.Protokoly.Update(protokol);
            _context.SaveChanges();

            return Ok();
        }
    }
}
