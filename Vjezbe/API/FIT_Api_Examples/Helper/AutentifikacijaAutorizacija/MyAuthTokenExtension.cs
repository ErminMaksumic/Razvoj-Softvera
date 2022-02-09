using System.Linq;
using System.Text.Json.Serialization;
using AutoMapper.QueryableExtensions;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FIT_Api_Examples.Helper.AutentifikacijaAutorizacija
{
    public static class MyAuthTokenExtension
    {
        public class LoginInformacije
        {
            public LoginInformacije(AutentifikacijaToken autentifikacijaToken)
            {
                this.autentifikacijaToken = autentifikacijaToken;
            }

            [JsonIgnore]
            public KorisnickiNalog korisnickiNalog => autentifikacijaToken?.korisnickiNalog;
            public AutentifikacijaToken autentifikacijaToken { get; set; }
            
            public bool isLogiran => korisnickiNalog != null;
            public bool isPermisijaStudentskaSluzba => isLogiran && (korisnickiNalog.isStudentskaSluzba || korisnickiNalog.isProdekan || korisnickiNalog.isAdmin);
            public bool isPermisijaDekan => isLogiran && (korisnickiNalog.isDekan || korisnickiNalog.isAdmin);
            public bool isPermisijaProdekan => isLogiran && (korisnickiNalog.isProdekan || korisnickiNalog.isDekan || korisnickiNalog.isAdmin);
            public bool isPermisijaNastavnik => isLogiran && (korisnickiNalog.nastavnik != null || korisnickiNalog.isAdmin);
            public bool isPermisijaStudent => isLogiran && (korisnickiNalog.student != null || korisnickiNalog.isAdmin);
            public bool isPermisijaAdmin => isLogiran && korisnickiNalog.isAdmin;
        }


        public static LoginInformacije GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInformacije(token);
        }
    
        public static AutentifikacijaToken GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();

            AutentifikacijaToken korisnickiNalog = db.AutentifikacijaToken
                .Include(s=>s.korisnickiNalog)
                .SingleOrDefault(x => token != null && x.vrijednost == token);
            
            return korisnickiNalog;
        }


        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }
    }
}
