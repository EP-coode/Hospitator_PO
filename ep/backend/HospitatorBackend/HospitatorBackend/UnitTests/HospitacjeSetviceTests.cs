using NUnit.Framework;
using HospitatorBackend.Services.Interfaces;
using HospitatorBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using HospitatorBackend.Models;
using HospitatorBackend.Services;

namespace HospitatorBackend.UnitTests
{
    [TestFixture]
    public class HospitacjeSetviceTests
    {
        private HospitatorDBContext context;

        [SetUp]
        public void Setup()
        {
            context = SeedInMemoryDatabase.GetcontextDatabaseContext();
        }

        [TestCase]
        public void PobieranieHospitacjiZespolu()
        {
            var hostpitacjeService = new HospitacjeService(context);

            var result = hostpitacjeService.GetHospitacjeZespolu(2);

            Assert.AreEqual(1, result.Count());
        }

        [TestCase]
        public void DodawanieFormularza()
        {
            var hostpitacjeService = new HospitacjeService(context);

            hostpitacjeService.DodajRaportHospitacji(new Dtos.FormularzProtokoluInputDto()
            {
                HospitacjaId = 2,
                LiczbaObecnych = 12,
                OcenaKoncowa = "3.5",
                Opuznienie = 10,
                Punktualnie = false,
                SalaPrzystosowana = false,
                PowodyNieprzystosowania = "powody",
                TrescKursuZgodna = true,
                SprawdzonoObecnosc = true
            });

            var result = context.Protokoly
                .Include(p => p.Formulazprotokolus)
                .FirstOrDefault(p => p.HospitacjaId == 2);

            Assert.NotNull(result);
            Assert.NotNull(result.Formulazprotokolus);
            Assert.AreEqual(12, result.Formulazprotokolus.LiczbaObecnych);
        }

    }
}
