using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Modul4_MaticnaKnjiga.Models
{
    public class AkademskaGodina
    {
        [Key]
        public int id{ get; set; }
        public string opis { get; set; }
        public KorisnickiNalog evidentiraoKorisnik { get; internal set; }
        public DateTime? datum_update { get; set; }
        public KorisnickiNalog izmijenioKorisnik { get; set; }
        public DateTime datum_added { get; set; }
    }
}
