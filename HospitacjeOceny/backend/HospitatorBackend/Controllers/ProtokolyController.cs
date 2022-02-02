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
using HospitatorBackend.Services;
using HospitatorBackend.Services.Interfaces;

namespace HospitatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtokolyController : ControllerBase
    {
        private readonly IHospitacjeService hospitacjeService;

        public ProtokolyController(IHospitacjeService hospitacjeService)
        {
            this.hospitacjeService = hospitacjeService;
        }

        // POST: api/Protokoly
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProtokolDto>> PostProtokol(FormularzProtokoluInputDto protokol)
        {
            var result = hospitacjeService.DodajRaportHospitacji(protokol);
            
            if(result == null)
            {
                return BadRequest();
            }

            // tu powinien byc endpoint z zasobem ale nie stworzono go
            return Created("", result);
        }
       
    }
}
