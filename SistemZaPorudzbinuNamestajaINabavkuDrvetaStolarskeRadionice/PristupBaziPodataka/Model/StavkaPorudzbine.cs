using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model
{
    public class StavkaPorudzbine
    {
        public int RedniBrojStavke { get; set; }
        public int Kolicina { get; set; }

        public string PorudzbinaMusterijaId { get; set; }
        public int PorudzbinaId { get; set; }
        public Porudzbina Porudzbina { get; set; }

        public int StavkaCenovnikaId { get; set; }
        public int RedniBrojStavkeCenovnika { get; set; }
        public StavkaCenovnika StavkaCenovnika { get; set; }
    }
}
