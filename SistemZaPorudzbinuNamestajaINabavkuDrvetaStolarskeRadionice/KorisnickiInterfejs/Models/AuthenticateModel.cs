using KorisnickiInterfejs.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Models
{
    public class AuthenticateModel : IEntityModel
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }


        public AuthenticateModel(string korisnickoIme = "", string lozinka = "")
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
        }
    }
}
