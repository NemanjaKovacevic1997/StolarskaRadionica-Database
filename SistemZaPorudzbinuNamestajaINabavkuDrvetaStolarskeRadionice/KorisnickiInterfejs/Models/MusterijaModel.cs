using KorisnickiInterfejs.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Models
{
    public class MusterijaModel : IEntityModel
    {
        public string JMBG { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public MusterijaModel(string jmbg = "", string ime = "", string prezime = "")
        {
            JMBG = jmbg;
            Ime = ime;
            Prezime = prezime;
        }
    }
}
