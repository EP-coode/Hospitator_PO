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
    public class ZespolHospitujcyController : ControllerBase
    {
        private readonly HospitatorDBContext _context;

        public ZespolHospitujcyController(HospitatorDBContext context)
        {
            _context = context;
        }

        // GET: api/Zespolhospitujacies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZespolHospitujacyDto>>> GetZespolhospitujacies()
        {
            var query = from zespol in _context.Zespolhospitujacies
                        select new ZespolHospitujacyDto()
                        {
                            Id = zespol.Id,
                            Przewodniczacy = new ProwadzacyDto()
                            {
                                Id = zespol.Prowadzacy.Id,
                                Imie = zespol.Prowadzacy.Imie,
                                Nazwisko = zespol.Prowadzacy.Nazwisko,
                                Tytol = zespol.Prowadzacy.Tytol
                            },
                            Hospitacje = zespol.Hospitacjas,
                            Sklad = zespol.Prowadzacies
                        };

            var result = await query.ToListAsync();

            return Ok(result);

        }

    }
}
