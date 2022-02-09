using System;
using System.Collections.Generic;
using System.Linq;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul3.Models;
using FIT_Api_Examples.Modul3.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FIT_Api_Examples.Modul3.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IspitController: ControllerBase
    {
        private ApplicationDbContext _dbContext;

        public IspitController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<Ispit>> GetByDatum(DateTime? periodOd, DateTime? periodDo)
        {
            List<Ispit> podaci = _dbContext.Ispit
                .Where(x=> periodOd ==null || x.DatumIspita>= periodOd )
                .Where(x=> periodDo == null || x.DatumIspita <= periodDo)
                .ToList();

            return podaci;
        }
        [HttpPost]
        public ActionResult AddUrlParam(DateTime datumIspita, int predmetID, string naziv)
        {

            if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
                return Forbid();
            
            Ispit ispit = new Ispit
            {
                DatumIspita = datumIspita,
                Naziv = naziv,
                PredmetID =  predmetID
            };
            _dbContext.Add(ispit);
            _dbContext.SaveChanges();//izvršava potreban SQL

            return Ok();
        }

       
        [HttpPost]
        public ActionResult AddJson([FromBody]IspitAddVM x)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
                return BadRequest("nije logiran");

            Ispit ispit = new Ispit
            {
                DatumIspita = x.datumIspita,
                Naziv = x.naziv,
                PredmetID = x.predmetID
            };
            _dbContext.Add(ispit);
            _dbContext.SaveChanges();//izvršava potreban SQL

            return Ok();
        }
    }
}
