using HospitatorBackend.Data;
using HospitatorBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.UnitTests
{
    public class SeedInMemoryDatabase
    {
        static public HospitatorDBContext GetcontextDatabaseContext()
        {
            DbContextOptions<HospitatorDBContext> dbContextOptions = new DbContextOptionsBuilder<HospitatorDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            var context = new HospitatorDBContext(dbContextOptions);
            context.Database.EnsureDeleted();

            context.Prowadzacy.Add(new Prowadzacy()
            {
                Id = 1,
                Imie = "Jan",
                Nazwisko = "Nowak",
                DataOstHospitacji = DateOnly.FromDateTime(DateTime.UtcNow),
                Doswiadczony = false,
                Habilitowany = false,
                StopienNaukowy = "doktor",
                Uznany = false,
                Tytol = "dr.",
                Stanowisko = ""
            });

            context.Prowadzacy.Add(new Prowadzacy()
            {
                Id = 2,
                Imie = "Dorota",
                Nazwisko = "Welman",
                DataOstHospitacji = DateOnly.FromDateTime(DateTime.UtcNow),
                Doswiadczony = true,
                Habilitowany = true,
                StopienNaukowy = "doktor",
                Uznany = true,
                Tytol = "dr.",
                Stanowisko = ""
            });

            context.Prowadzacy.Add(new Prowadzacy()
            {
                Id = 3,
                Imie = "Bartosz",
                Nazwisko = "Walaszel",
                DataOstHospitacji = DateOnly.FromDateTime(DateTime.UtcNow),
                Doswiadczony = false,
                Habilitowany = false,
                StopienNaukowy = "kapitan",
                Uznany = false,
                Tytol = "dr.",
                Stanowisko = ""
            });

            context.Prowadzacy.Add(new Prowadzacy()
            {
                Id = 4,
                Imie = "Rick",
                Nazwisko = "Sanchez",
                DataOstHospitacji = DateOnly.FromDateTime(DateTime.UtcNow),
                Doswiadczony = true,
                Habilitowany = true,
                StopienNaukowy = "doktor",
                Uznany = true,
                Tytol = "dr.",
                Stanowisko = ""
            });

            var zespol = new Zespolhospitujacy()
            {
                Id = 1,
                ProwadzacyId = 2, // dorota welman
            };

            context.ZespolyHospitujace.Add(zespol);

            context.Prowadzacy_ZespolHospitujacy.Add(new Prowadzacy_ZespolHospitujacy()
            {
                ProwadzacyID = 3, // Bartosz Walaszek
                ZespolID = 1,
            });

            context.Prowadzacy_ZespolHospitujacy.Add(new Prowadzacy_ZespolHospitujacy()
            {
                ProwadzacyID = 4, // Bartosz Walaszek
                ZespolID = 1,
            });

            Harmonogram harmonogram = new()
            {
                Id = 1,
                ZatwierdzonyDyrektor = true,
                ZatwierdzonyWkozjk = true
            };

            context.Harmonogramy.Add(harmonogram);

            context.Kursy.Add(new Kurs()
            {
                Kod = "abc-123",
                Forma = "wyklad",
                Nazwa = "Techniki prezentacji",
                Semestr = 5
            });

            context.Hospitacje.Add(new Hospitacja()
            {
                Id = 1,
                HarmonogramId = 1,
                KursKod = "abc-123",
                ProwadzacyId = 1,
                Termin = DateOnly.FromDateTime(DateTime.UtcNow),
                ZespolHospitujacyId = 1,
            });

            context.Hospitacje.Add(new Hospitacja()
            {
                Id = 2,
                HarmonogramId = 1,
                KursKod = "abc-123",
                ProwadzacyId = 1,
                Termin = DateOnly.FromDateTime(DateTime.UtcNow),
                ZespolHospitujacyId = 1,
            });

            context.Protokoly.Add(new Protokol()
            {
                Id = 1,
                HospitacjaId = 1,
                DataWystawienia = DateOnly.FromDateTime(DateTime.UtcNow),
            });

            Formulazprotokolu f = new()
            {
                Id = 1,
                ProtokolId = 1,
                LiczbaObecnych = 10,
                OcenaKoncowa = "4.5",
                Opuznienie = 10,
                PowodyNieprzystosowania = "Stary sprzet na sali",
                Punktualnie = false,
                SalaPrzystosowana = false,
                SprawdzonoObecnosc = true,
                TrescKursuZgodna = true,
            };

            context.SaveChanges();

            context.Add(f);

            context.SaveChanges();

            return context;
        }
    }
}
