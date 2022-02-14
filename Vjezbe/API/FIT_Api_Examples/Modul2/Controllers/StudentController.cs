using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul2.ViewModels;
using FIT_Api_Examples.Modul3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul2.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        public class StudentVM
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public int OpstinaRodjenjaId { get; set; }
        }
        private readonly ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<PagedList<Student>> GetAllPaged(string ime_prezime, int items_per_page, int page_number = 1)
        {

            var data = _dbContext.Student
                .Include(s => s.opstina_rodjenja.drzava)
                .Where(x => ime_prezime == null || (x.ime + " " + x.prezime).StartsWith(ime_prezime) || (x.prezime + " " + x.ime).StartsWith(ime_prezime)).OrderByDescending(s => s.prezime).ThenByDescending(s => s.ime)
                .AsQueryable();
            return PagedList<Student>.Create(data, page_number, items_per_page);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAll()
        {
            return _dbContext.Student.Include(x=> x.opstina_rodjenja).ThenInclude(x=> x.drzava).ToList();
        }

        [HttpPost]
        public ActionResult<int> Add([FromBody] StudentVM student)
        {
            if(!HttpContext.GetLoginInfo().isLogiran)
            {
                return Unauthorized();
            }

            if(student==null)
            {
                return BadRequest();
            }

            var noviStudent = new Student()
            {
                ime = student.Ime,
                prezime = student.Prezime,
                opstina_rodjenja_id = student.OpstinaRodjenjaId
            };

            _dbContext.Add(noviStudent);

            _dbContext.SaveChanges();

            return noviStudent.id;
        }
        
        [HttpPut]
        public ActionResult Edit([FromBody] StudentVM xs)
        {
            var student = _dbContext.Student.SingleOrDefault(x => x.id == xs.Id);

            if (student == null) return NotFound();

            student.ime = xs.Ime;
            student.prezime = xs.Prezime;
            student.opstina_rodjenja_id = xs.OpstinaRodjenjaId;

            _dbContext.SaveChanges();

            return Ok();

        }


    }
}
