using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model
{
    public class Musterija
    {
        public string JMBG { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public ICollection<Porudzbina> Porudzbine { get; set; }
    }
}
