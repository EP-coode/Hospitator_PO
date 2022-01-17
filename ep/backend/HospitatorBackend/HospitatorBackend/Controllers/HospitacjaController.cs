#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitatorBackend.Data;
using HospitatorBackend.Models;
using HospitatorBackend.Dtos;

namespace HospitatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitacjaController : ControllerBase
    {
        private readonly HospitatorDBContext _context;

        public HospitacjaController(HospitatorDBContext context)
        {
            _context = context;
        }

        // GET: Hospitacja
        [HttpGet("{id_przewodniczacego:int}")]
        public async Task<ICollection<HospitacjaDto>> GetHospitacjeByPrzewodniczacy(int id_przewodniczacego)
        {
            var hospitatorDBContext = _context.Hospitacje
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


            return await hospitatorDBContext.ToListAsync();
        }
    }
}
