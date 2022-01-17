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
    public class ProwadzacyController : ControllerBase
    {
        private readonly HospitatorDBContext _context;

        public ProwadzacyController(HospitatorDBContext context)
        {
            _context = context;
        }

        // GET: api/Prowadzacy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProwadzacyDto>>> GetProwadzacy()
        {
            return await _context.Prowadzacy.Select(p => p.ToDto()).ToListAsync();
        }

        // GET: api/Prowadzacy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProwadzacyDto>> GetProwadzacy(int id)
        {
            var prowadzacy = await _context.Prowadzacy.FindAsync(id);

            if (prowadzacy == null)
            {
                return NotFound();
            }

            return prowadzacy.ToDto();
        }
    }
}
