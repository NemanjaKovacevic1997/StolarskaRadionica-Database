using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model
{
    public class StavkaCenovnika
    {
        public int RedniBrojStavke { get; set; }
        public double Cena { get; set; }

        public ICollection<StavkaPorudzbine> StavkePorudzbine { get; set; }

        public int VrstaNamestajaId { get; set; }
        public VrstaNamestaja VrstaNamestaja { get; set; }
    }
}
