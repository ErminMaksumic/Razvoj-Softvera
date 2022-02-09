using System;
using System.Collections.Generic;
using System.Linq;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul3.Models;
using FIT_Api_Examples.Modul4_MaticnaKnjiga.Models;
using FIT_Api_Examples.Modul4_MaticnaKnjiga.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul4_MaticnaKnjiga.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AkademskeGodineController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AkademskeGodineController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost("{id}")]
        public ActionResult<AkademskaGodina> Snimi(int id, [FromBody] AkademskagodinaAddVM x)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaProdekan)
                return BadRequest("nije logiran");

            AkademskaGodina akademskaGodina;
            if (id == 0)
            {
                akademskaGodina = new AkademskaGodina()
                {
                   opis= x.opis,
                   datum_added = DateTime.Now,
                    evidentiraoKorisnik = HttpContext.GetLoginInfo().korisnickiNalog,
                };
                _dbContext.Add(akademskaGodina);
            }
            else
            {
                akademskaGodina = _dbContext.AkademskaGodina.FirstOrDefault(s => s.id == id);
                if (akademskaGodina == null)
                    return BadRequest("pogresan id");
                akademskaGodina.datum_update = DateTime.Now;
                akademskaGodina.izmijenioKorisnik = HttpContext.GetLoginInfo().korisnickiNalog;
            }

            akademskaGodina.opis = x.opis;
           
                
            _dbContext.SaveChanges();
            return akademskaGodina;
        }

        [HttpGet]
        public List<CmbStavke> GetAll_ForCmb()
        {
            return _dbContext.AkademskaGodina
                .OrderByDescending(x => x.id)
                .Select(s=>new CmbStavke
                {
                    opis = s.opis,
                    id = s.id
                })
                .ToList();
        }
        
        [HttpGet]
        public List<AkademskaGodina> GetAll()
        {
            return _dbContext.AkademskaGodina
                .OrderByDescending(x=>x.id)
                .ToList();
        }
    }
}
