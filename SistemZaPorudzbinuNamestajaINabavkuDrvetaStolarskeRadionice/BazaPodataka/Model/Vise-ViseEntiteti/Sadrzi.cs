using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model.Vise_ViseEntiteti
{
    public class Sadrzi
    {
        public int VrstaDrvetaId { get; set; }
        public VrstaDrveta VrstaDrveta { get; set; }

        public int ZahtevId { get; set; }
        public Zahtev Zahtev { get; set; }

        public int Kolicina { get; set; }

    }
}
