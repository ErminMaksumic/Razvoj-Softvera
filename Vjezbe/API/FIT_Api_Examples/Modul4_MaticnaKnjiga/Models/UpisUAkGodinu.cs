using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;

namespace FIT_Api_Examples.Modul4_MaticnaKnjiga.Models
{
    public class UpisUAkGodinu
    {
        [Key]
        public int id{ get; set; }
        public DateTime? datum4_LjetniOvjera { get; set; }
        public DateTime? datum3_LjetniUpis { get; set; }
        public DateTime? datum2_ZimskiOvjera { get; set; }
        public DateTime? datum1_ZimskiUpis { get; set; }
        public int godinaStudija { get; set; }
        
        
        [ForeignKey(nameof(student))]
        public int studentId { get; set; }
        public Student student { get; set; }
        
        

        [ForeignKey(nameof(akademskaGodina))]
        public int akademskaGodinaId { get; set; }
        public AkademskaGodina akademskaGodina { get; set; }
        
        public float? cijenaSkolarine { get; set; }


        [ForeignKey(nameof(evidentiraoKorisnik))]
        public int evidentiraoKorisnikId { get; set; }
        public KorisnickiNalog evidentiraoKorisnik{ get; set; }

        public bool obnovaGodine{ get; set; }
    }
}
