using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul3.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIT_Api_Examples.Modul3.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class NastavnikController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public NastavnikController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<Nastavnik> Add([FromBody] NastanvikAddVM x)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdmin)
                return BadRequest("nije logiran");

            var nastavnik = new Nastavnik()
            {
                ime = x.ime,
                prezime = x.prezime,
                korisnickoIme = x.korisnickoIme,
            };

            _dbContext.Add(nastavnik);
            _dbContext.SaveChanges();
            return nastavnik;
        }

        [HttpGet]
        public List<Nastavnik> GetAll()
        {
            var data = _dbContext.Nastavnik
                .OrderBy(s => s.prezime + " " + s.ime)
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpGet]
        public List<CmbStavke> GetAllCmb()
        {
            var data = _dbContext.Nastavnik
                .OrderBy(s => s.prezime + " " + s.ime)
                .Select(s => new CmbStavke()
                {
                    id = s.id,
                    opis = s.prezime + " " + s.ime,
                })
                .AsQueryable();
            return data.Take(100).ToList();
        }
    }
}
