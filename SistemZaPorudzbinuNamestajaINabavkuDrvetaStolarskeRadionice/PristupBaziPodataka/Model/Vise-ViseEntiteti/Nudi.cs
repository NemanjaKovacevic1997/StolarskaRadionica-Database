using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model.Vise_ViseEntiteti
{
    public class Nudi
    {
        public int VrstaDrvetaId { get; set; }
        public VrstaDrveta VrstaDrveta { get; set; }

        public int DobavljacDrvetaId { get; set; }
        public DobavljacDrveta DobavljacDrveta { get; set; }

        public double CenaPoKubnomMetru { get; set; }
        public int Kolicina { get; set; }
    }
}
