using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model
{
    public class Cenovnik
    {
        public int Id { get; set; }  //nije bitan jer je zapravo StolarskaRadionicaId kljuc.
        public ICollection<StavkaCenovnika> StavkeCenovnika { get; set; }

        public int? StolarId { get; set; }
        public Stolar Stolar { get; set; }

        public int StolarskaRadionicaId { get; set; }
        public StolarskaRadionica StolarskaRadionica { get; set; }
    }
}
