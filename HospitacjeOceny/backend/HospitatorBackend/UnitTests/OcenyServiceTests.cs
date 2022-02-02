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
    public class OcenyServiceTests
    {
        private IOcenyService ocenyService;
        private HospitatorDBContext context;
        private static string uzasadanienie = "uzasadnienie reklamacji";

        [SetUp]
        public void Setup()
        {
            context = SeedInMemoryDatabase.GetcontextDatabaseContext();
        }

        [TestCase]
        public void TestAkceptacjaOceny()
        {
            var ocenyService = new OcenyService(context);

            var p = context.Protokoly.First(p => p.Id == 1);

            ocenyService.ZakceptujOcene(1, 1);

            var result = context.Protokoly.First(p => p.Id == 1);

            Assert.IsTrue(result.Zakceptowane);
            Assert.IsNull(result.Odwolanie);
        }

        [TestCase]
        public void TestReklamacjaOceny()
        {
            var ocenyService = new OcenyService(context);

            ocenyService.ZareklamujOcene(new Dtos.ReklamacjaDto()
            {
                ProtokolId = 1,
                ProwadzacyId = 1,
                Uzasadnienie = uzasadanienie
            });

            var result = context.Protokoly
                .Include(p => p.Odwolanie)
                .First(p => p.Id == 1);

            Assert.IsFalse(result.Zakceptowane);
            Assert.IsNotNull(result.Odwolanie);
            Assert.AreEqual(uzasadanienie, result.Odwolanie.Uzasadnienie);
        }

        [TestCase]
        public void TestZakceptowanaOcenaNieMozeBycReklamowana()
        {
            var ocenyService = new OcenyService(context);

            var p = context.Protokoly.First(p => p.Id == 1);

            ocenyService.ZakceptujOcene(1, 1);

            // var t =  context.Protokoly
            //     .Include(p => p.Hospitacja)
            //     .Include(p => p.Odwolanie)
            //         .Where(p =>
            //      p.Odwolanie == null
            //     && p.Zakceptowane == true).ToList();

            ocenyService.ZareklamujOcene(new Dtos.ReklamacjaDto()
            {
                ProtokolId = 1,
                ProwadzacyId = 1,
                Uzasadnienie = uzasadanienie
            });

            var result = context.Protokoly
                .Include(p => p.Odwolanie)
                .First(p => p.Id == 1);

            Assert.IsTrue(result.Zakceptowane);
            Assert.IsNull(result.Odwolanie);
        }
    }
}
