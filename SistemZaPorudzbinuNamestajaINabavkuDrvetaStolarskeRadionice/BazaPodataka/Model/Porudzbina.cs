using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model
{
    public class Porudzbina
    {
        public int Id { get; set; }
        public DateTime DatumPorudzbine { get; set; }
        public double Ukupno { get; set; }

        public string MusterijaId { get; set; }
        public Musterija Musterija { get; set; }

        public ICollection<StavkaPorudzbine> StavkePorudzbine { get; set; }
    }
}
