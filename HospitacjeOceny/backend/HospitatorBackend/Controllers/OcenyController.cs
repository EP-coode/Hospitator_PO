using HospitatorBackend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HospitatorBackend.Dtos;
using HospitatorBackend.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using HospitatorBackend.Services.Interfaces;

namespace HospitatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcenyController : ControllerBase
    {
        private readonly IOcenyService ocenyService;

        public OcenyController(IOcenyService ocenyService)
        {
            this.ocenyService = ocenyService;
        }

        [HttpGet("{id_prowadzacego:int}")]
        public ActionResult<PrzegladOcenDto> GetOcenyNauczycielaById(int id_prowadzacego)
        {
            var nowe = ocenyService.GetNoweOcenyProwadzacego(id_prowadzacego);
            var zakceptowane = ocenyService.GetZakceptowaneOcenyProwadzacego(id_prowadzacego);
            var zareklamowane = ocenyService.GetZakceptowaneOcenyProwadzacego(id_prowadzacego);

            return Ok(new PrzegladOcenDto()
            {
                Nowe = nowe,
                Zakceptowane = zakceptowane,
                Zareklamowane = zareklamowane,
            });
        }


        [HttpPost("Reklamuj")]
        public ActionResult<ReklamacjaDto> ZareklamujOcene(ReklamacjaDto r)
        {
            Odwolanie? o = ocenyService.ZareklamujOcene(r);

            if(o == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("Akceptuj/{id_nauczyciela:int}/{id_protokolu:int}")]
        public async Task<ActionResult<ProtokolDto>> ZakceptujOcene(int id_nauczyciela, int id_protokolu)
        {
            Protokol? p = ocenyService.ZakceptujOcene(id_nauczyciela, id_protokolu);

            if(p == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
