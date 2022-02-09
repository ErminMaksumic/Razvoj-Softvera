using System;
using System.Collections.Generic;
using System.Linq;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul3.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul2.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class ObavijestController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ObavijestController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost("{id}")]
        public ActionResult<Obavijest> Snimi(int id, [FromBody] ObavijestAddVM x)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaProdekan)
                return BadRequest("nije logiran");

            Obavijest obavijest;
            if (id == 0)
            {
                obavijest = new Obavijest
                {
                   datum_kreiranja=DateTime.Now,
                   evidentiraoKorisnik = HttpContext.GetLoginInfo().korisnickiNalog,
                };
                _dbContext.Add(obavijest);
            }
            else
            {
                obavijest = _dbContext.Obavijest.FirstOrDefault(s => s.id == id);
                if (obavijest == null)
                    return BadRequest("pogresan id");
                obavijest.datum_update = DateTime.Now;
                obavijest.izmijenioKorisnik = HttpContext.GetLoginInfo().korisnickiNalog;
            }

            obavijest.naslov = x.naslov;
            obavijest.tekst = x.tekst;
           
                
            _dbContext.SaveChanges();
            return GetById(obavijest.id);
        }

        private ActionResult<Obavijest> GetById(int id)
        {
            Obavijest obavijest = _dbContext.Obavijest
                .Include(s=>s.izmijenioKorisnik)
                .Include(s=>s.evidentiraoKorisnik)
                .FirstOrDefault(s => s.id == id);
            return obavijest;
        }

        [HttpGet]
        public List<Obavijest> GetAll()
        {
            var data = _dbContext.Obavijest
                .Include(s => s.izmijenioKorisnik)
                .Include(s => s.evidentiraoKorisnik)
                .OrderByDescending(x=>x.id)
                .AsQueryable();
            return data.Take(100).ToList();
        }
    }
}
