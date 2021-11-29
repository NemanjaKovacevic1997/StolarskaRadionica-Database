using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model.Vise_ViseEntiteti
{
    public class ImaPonudu
    {
        public int StolarskaRadionicaId { get; set; }
        public int DobavljacDrvetaSaradjujeId { get; set; }
        public SaradjujeSa SaradjujeSa { get; set; }


        public int DobavljacDrvetaNudiId { get; set; }
        public int VrstaDrvetaId { get; set; }
        public Nudi Nudi { get; set; }
    }
}
