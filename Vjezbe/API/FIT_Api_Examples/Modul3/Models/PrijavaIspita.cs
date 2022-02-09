using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using FIT_Api_Examples.Modul2.Models;

namespace FIT_Api_Examples.Modul3.Models
{
    public class PrijavaIspita
    {
        public int	ID             {get;set;}
        public DateTime	DatumPrijave   {get;set;}
        
        
        [ForeignKey(nameof(StudentID))]
        public Student Student { get; set; }
        public int	StudentID      {get;set;}


        [ForeignKey(nameof(IspitID))]
        public Ispit Ispit{ get; set; }
        public int IspitID { get; set; }


    }
}
