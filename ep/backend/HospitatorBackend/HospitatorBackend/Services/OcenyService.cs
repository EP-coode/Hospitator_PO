using HospitatorBackend.Data;
using HospitatorBackend.Dtos;
using HospitatorBackend.Models;
using HospitatorBackend.Services.Interfaces;

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


            return query_nowe.ToList();
        }

        public ICollection<ProtokolDto> GetZakceptowaneOcenyProwadzacego(int id_prowadzacego)
        {
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
                                         Odwolanie = p.Odwolanie,
                                         Kurs = p.Hospitacja.KursKodNavigation,
                                     };
            return query_zakceptowane.ToList();
        }

        public ICollection<ProtokolDto> GetZareklamowaneOcenyProwadzacego(int id_prowadzacego)
        {
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
                                          Odwolanie = p.Odwolanie,
                                          Kurs = p.Hospitacja.KursKodNavigation,
                                      };
            return query_zareklamowane.ToList();
        }
    }
}

