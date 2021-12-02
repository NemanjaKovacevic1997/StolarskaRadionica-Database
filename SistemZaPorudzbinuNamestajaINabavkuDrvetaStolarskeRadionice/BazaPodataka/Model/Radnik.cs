using BazaPodataka.Model.Vise_ViseEntiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model
{
    public class Radnik
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Zanimanje { get; set; }
        public int StolarskaRadionicaId { get; set; }
        public StolarskaRadionica StolarskaRadionica { get; set; }

        public ICollection<Skladisti> StavkeCenovnika { get; set; }
    }
}
