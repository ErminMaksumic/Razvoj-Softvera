using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul2.Models;
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
    public class MaticnaKnjigaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public MaticnaKnjigaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult AddGodina(int id, [FromBody] AkademskagodinaAddVM a)
        {
            if (HttpContext.GetLoginInfo().isLogiran)
                return Unauthorized();

            var student = _dbContext.Student.SingleOrDefault(x => x.id == id);

            if (_dbContext.UpisUAkGodinu.Where(x => x.godinaStudija == a.GodinaStudija
            && student.id == id && a.obnovaGodine == false).Count()>0)
                return BadRequest("Ne mozete dodati istu godinu bez obnove!");


            _dbContext.UpisUAkGodinu.Add(new UpisUAkGodinu()
            {
                datum1_ZimskiUpis = a.Datum,
                cijenaSkolarine = a.CijenaSkolarine,
                akademskaGodinaId = a.AkademskaGodinaId,
                obnovaGodine = a.obnovaGodine,
                godinaStudija = a.GodinaStudija,
                studentId = id,
                evidentiraoKorisnik = HttpContext.GetLoginInfo().korisnickiNalog
            });;

            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult GetByStudent(int id)
        {
            if (HttpContext.GetLoginInfo().isLogiran)
                return Unauthorized();

            var student = _dbContext.UpisUAkGodinu.Include(x=>x.evidentiraoKorisnik).Include(x=>x.akademskaGodina).Where(x => x.studentId == id).ToList();

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public ActionResult OvjeriSemestar([FromBody] int godinaId)
        {
            var godina = _dbContext.UpisUAkGodinu.SingleOrDefault(x => x.id == godinaId);

            if(godina!=null)
            {
                godina.datum2_ZimskiOvjera = DateTime.Now;
                return Ok(_dbContext.SaveChanges());
            }
            return NotFound();
        }
    }
}
