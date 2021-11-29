using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model.Vise_ViseEntiteti
{
    public class Skladisti
    {
        public int VrstaDrvetaId { get; set; }
        public VrstaDrveta VrstaDrveta { get; set; }

        public int MagacinId { get; set; }
        public Magacin Magacin { get; set; }

        public int Kolicina { get; set; }
    }
}
