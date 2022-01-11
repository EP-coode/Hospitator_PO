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

namespace HospitatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtokolyController : ControllerBase
    {
        private readonly HospitatorDBContext _context;

        public ProtokolyController(HospitatorDBContext context)
        {
            _context = context;
        }

        // GET: api/Protokoly
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Protokol>>> GetProtokols()
        {
            return await _context.Protokols.ToListAsync();
        }

        // POST: api/Protokoly
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Protokol>> PostProtokol(Protokol protokol)
        {
            _context.Protokols.Add(protokol);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProtokol", new { id = protokol.Id }, protokol);
        }
    }
}
