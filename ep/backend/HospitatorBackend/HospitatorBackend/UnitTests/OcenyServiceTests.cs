using NUnit.Framework;
using HospitatorBackend.Services.Interfaces;
using HospitatorBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using HospitatorBackend.Models;

namespace HospitatorBackend.UnitTests
{
    [TestFixture]
    public class OcenyServiceTests
    {
        private DbContextOptions<HospitatorDBContext> dbContextOptions = new DbContextOptionsBuilder<HospitatorDBContext>()
            .UseInMemoryDatabase(databaseName: "PrimeDb")
            .Options;

        private IOcenyService ocenyService;
        private HospitatorDBContext context;

        [SetUp]
        public void Setup()
        {
            context = new HospitatorDBContext(dbContextOptions);
        }

        //[TearDown]
        //public void CleanUp()
        //{

        //}

        [TestCase]
        public void TestDodawanieProwadzacego()
        {
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

            context.SaveChanges();

            var found = context.Prowadzacy.Find(1);

            if(false == null)
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
        }

        [TestCase]
        public void TestAkceptacjaOceny()
        {
            Assert.Pass();
        }

        [TestCase]
        public void TestReklamacjaOceny()
        {
            Assert.Fail();
        }
    }
}
