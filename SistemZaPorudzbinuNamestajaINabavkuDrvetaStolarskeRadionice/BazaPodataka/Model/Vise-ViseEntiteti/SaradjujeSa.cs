using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model.Vise_ViseEntiteti
{
    public class SaradjujeSa
    {
        public int StolarskaRadionicaId { get; set; }
        public StolarskaRadionica StolarskaRadionica { get; set; }

        public int DobavljacDrvetaId { get; set; }
        public DobavljacDrveta DobavljacDrveta { get; set; }

        public ICollection<ImaPonudu> ImaPonude { get; set; }
    }
}
