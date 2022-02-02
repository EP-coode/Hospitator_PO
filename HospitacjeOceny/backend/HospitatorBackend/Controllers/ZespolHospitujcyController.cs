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
    //[Route("api/[controller]")]
    //[ApiController]
    //public class ZespolHospitujcyController : ControllerBase
    //{
    //    private readonly HospitatorDBContext _context;

    //    public ZespolHospitujcyController(HospitatorDBContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: api/Zespolhospitujacies
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<ZespolHospitujacyDto>>> GetZespolhospitujacies()
    //    {
    //        var query = from zespol in _context.ZespolyHospitujace
    //                    select new ZespolHospitujacyDto()
    //                    {
    //                        Id = zespol.Id,
    //                        Przewodniczacy = new ProwadzacyDto()
    //                        {
    //                            Id = zespol.Prowadzacy.Id,
    //                            Imie = zespol.Prowadzacy.Imie,
    //                            Nazwisko = zespol.Prowadzacy.Nazwisko,
    //                            Tytol = zespol.Prowadzacy.Tytol
    //                        },
    //                        Hospitacje = zespol.Hospitacjas.Select(h => new HospitacjaDto()
    //                        {
    //                            Id = h.Id,
    //                            KursKod = h.KursKod,
    //                            Prowadzacy = h.Prowadzacy,
    //                            KursKodNavigation = h.KursKodNavigation,
    //                            Termin = h.Termin
    //                        }).ToList(),
    //                        Sklad = zespol.Prowadzacies
    //                    };

    //        var result = await query.ToListAsync();

    //        return Ok(result);

    //    }

    //    [HttpGet("{id_zespolu:int}")]
    //    public async Task<ActionResult<ZespolHospitujacyDto>> GetZespolById(int id_zespolu)
    //    {
    //        var zespol = _context.ZespolyHospitujace
    //              .Include(zh => zh.Prowadzacy)
    //              .Include(zh => zh.Prowadzacies)
    //              .FirstOrDefault(zh => zh.Id == id_zespolu);

    //        if(zespol == null)
    //        {
    //            return NotFound();
    //        }

    //        var hospitacje = _context.Hospitacje
    //            .Where(h => h.ZespolHospitujacyId == zespol.Id)
    //            .Include(h => h.Prowadzacy)
    //            .Select(h => new HospitacjaDto()
    //            {
    //                Id = h.Id,
    //                KursKod = h.KursKod,
    //                Prowadzacy = h.Prowadzacy,
    //                KursKodNavigation = h.KursKodNavigation,
    //                Termin = h.Termin
    //            })
    //            .ToList();

    //        return Ok(new ZespolHospitujacyDto()
    //        {
    //            Id = zespol.Id,
    //            Przewodniczacy = new ProwadzacyDto()
    //            {
    //                Id = zespol.Prowadzacy.Id,
    //                Imie = zespol.Prowadzacy.Imie,
    //                Nazwisko = zespol.Prowadzacy.Nazwisko,
    //                Tytol = zespol.Prowadzacy.Tytol
    //            },
    //            Hospitacje = hospitacje,
    //            Sklad = zespol.Prowadzacies
    //        });

    //    }

    //}
}
