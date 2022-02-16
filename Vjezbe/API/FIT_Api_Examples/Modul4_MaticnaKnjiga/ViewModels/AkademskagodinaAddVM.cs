using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Modul4_MaticnaKnjiga.ViewModels
{
    public class AkademskagodinaAddVM
    {
        public DateTime Datum { get; set; }
        public string Opis { get; set; }
        public int GodinaStudija { get; set; }
        public int AkademskaGodinaId { get; set; }
        public float CijenaSkolarine { get; set; }
        public bool ObnovaGodine { get; set; }
    }
}
