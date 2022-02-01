using HospitatorBackend.Data;
using HospitatorBackend.Dtos;
using HospitatorBackend.Models;
using HospitatorBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace HospitatorBackend.Services
{
    public class HospitacjeService : IHospitacjeService
    {
        private readonly HospitatorDBContext _context;

        public HospitacjeService(HospitatorDBContext context)
        {
            _context = context;
        }

        public ICollection<HospitacjaDto> GetHospitacjeZespolu(int id_przewodniczacego)
        {
            var query_hospitacje = _context.Hospitacje
                .Include(h => h.ZespolHospitujacy)
                .Include(h => h.Prowadzacy)
                .Include(h => h.Harmonogram)
                .Include(h => h.KursKodNavigation)
                .Include(h => h.Protokol)
                .Where(h => id_przewodniczacego == h.ZespolHospitujacy.ProwadzacyId && h.Protokol == null)
                .Select(h => new HospitacjaDto()
                {
                    Id = h.Id,
                    KursKod = h.KursKod,
                    KursKodNavigation = h.KursKodNavigation,
                    Prowadzacy = h.Prowadzacy,
                    Termin = h.Termin
                });


            return query_hospitacje.ToList();
        }

        public Protokol? DodajRaportHospitacji(FormularzProtokoluInputDto protokol)
        {
            var hospitacja = _context.Hospitacje.Find(protokol.HospitacjaId);

            if (hospitacja == null)
            {
                return null;
            }

            var istiejaProtokoly = _context.Protokoly.Any(p => p.HospitacjaId == protokol.HospitacjaId);

            if (istiejaProtokoly)
            {
                return null;
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

            return p;
        }

    }
}

