using HospitatorBackend.Data;
using HospitatorBackend.Dtos;
using HospitatorBackend.Models;
using HospitatorBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.Services
{

    public class OcenyService : IOcenyService
    {
        private readonly HospitatorDBContext _context;

        public OcenyService(HospitatorDBContext context)
        {
            _context = context;
        }

        public Odwolanie? ZareklamujOcene(ReklamacjaDto r)
        {
            var protokol = _context.Protokoly.First(p => p.Id == r.ProtokolId && p.Hospitacja.Prowadzacy.Id == r.ProwadzacyId);

            if (protokol == null)
            {
                return null;
            }

            var istniejeRekalmacja = _context.Odwolania.Any(o => o.ProtokolId == r.ProtokolId);

            if (istniejeRekalmacja)
            {
                return null;
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

            return rekalamcja;
        }

        public Protokol? ZakceptujOcene(int id_nauczyciela, int id_protokolu)
        {
            var protokol = _context.Protokoly
                .Where(p => p.Hospitacja.Prowadzacy.Id == id_nauczyciela && p.Odwolanie == null)
                .First(p => p.Id == id_protokolu);

            if (protokol == null)
            {
                return null;
            }

            protokol.Zakceptowane = true;

            _context.Protokoly.Update(protokol);
            _context.SaveChanges();

            return protokol;
        }

        public ICollection<ProtokolDto> GetNoweOcenyProwadzacego(int id_prowadzacego)
        {
            var query_nowe = _context.Protokoly
                            .Include(p => p.Hospitacja.KursKodNavigation)
                            .Include(p => p.Odwolanie)
                            .Include(p => p.Formulazprotokolus)
                            .Where(p => p.Hospitacja.ProwadzacyId == id_prowadzacego
                                && p.Zakceptowane == false
                                && p.Odwolanie == null)
                            .Select(p => p.ToDto());



            return query_nowe.ToList();
        }

        public ICollection<ProtokolDto> GetZakceptowaneOcenyProwadzacego(int id_prowadzacego)
        {
            var query_zakceptowane = _context.Protokoly
                .Include(p => p.Hospitacja.KursKodNavigation)
                .Include(p => p.Odwolanie)
                .Include(p => p.Formulazprotokolus)
                .Where(p => p.Hospitacja.ProwadzacyId == id_prowadzacego
                    && p.Zakceptowane == true)
                .Select(p => p.ToDto());

            return query_zakceptowane.ToList();
        }

        public ICollection<ProtokolDto> GetZareklamowaneOcenyProwadzacego(int id_prowadzacego)
        {

            var query_zareklamowane = _context.Protokoly
                .Include(p => p.Hospitacja.KursKodNavigation)
                .Include(p => p.Odwolanie)
                .Include(p => p.Formulazprotokolus)
                .Where(p => p.Hospitacja.ProwadzacyId == id_prowadzacego
                    && p.Odwolanie != null)
                .Select(p => p.ToDto());

            return query_zareklamowane.ToList();
        }
    }
}

