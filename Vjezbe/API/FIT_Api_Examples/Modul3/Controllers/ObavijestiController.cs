using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul3.Models;
using FIT_Api_Examples.Modul3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Modul3.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ObavijestiController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ObavijestiController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Add([FromBody] ObavijestVM x)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return Unauthorized();

            _dbContext.Obavijest.Add(new Obavijest()
            {
                datum_kreiranja = DateTime.Now,
                naslov = x.Naslov,
                tekst = x.Sadrzaj,
                evidentiraoKorisnik = HttpContext.GetLoginInfo().korisnickiNalog
            });

            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public int Update(int id, [FromBody] ObavijestVM x)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return -1;

            var obavijest = _dbContext.Obavijest.SingleOrDefault(x => x.id == id);

            obavijest.naslov = x.Naslov;
            obavijest.tekst = x.Sadrzaj;
            obavijest.izmijenioKorisnik = HttpContext.GetLoginInfo().korisnickiNalog;

            _dbContext.SaveChanges();

            return obavijest.id;
        }


        [HttpGet]
        public IEnumerable<Obavijest>GetAll()
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return null;

            return _dbContext.Obavijest
                .Include(x=>x.izmijenioKorisnik)
                .Include(x=>x.evidentiraoKorisnik)
                .ToList();
        }

        [HttpGet("{id}")]
        public Obavijest GetById(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return null;

            var obavijest = _dbContext.Obavijest.SingleOrDefault(x => x.id == id);

            if (obavijest == null)
                return null;

            return obavijest;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return;


            var obavijest = _dbContext.Obavijest.SingleOrDefault(x => x.id == id);

            if (obavijest == null)
                return;

            _dbContext.Obavijest.Remove(obavijest);

            _dbContext.SaveChanges();
        }
    }
}
