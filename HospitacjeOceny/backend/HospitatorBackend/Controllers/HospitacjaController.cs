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
using HospitatorBackend.Services;
using HospitatorBackend.Services.Interfaces;

namespace HospitatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitacjaController : ControllerBase
    {
        private readonly IHospitacjeService hospitacjeService;

        public HospitacjaController(IHospitacjeService hospitacjeService)
        {
            this.hospitacjeService = hospitacjeService;
        }

        // GET: Hospitacja
        [HttpGet("{id_przewodniczacego:int}")]
        public ActionResult<ICollection<HospitacjaDto>> GetHospitacjeByPrzewodniczacy(int id_przewodniczacego)
        {
            var result = hospitacjeService.GetHospitacjeZespolu(id_przewodniczacego);
            
            if(result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
