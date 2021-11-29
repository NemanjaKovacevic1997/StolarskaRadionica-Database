using BazaPodataka.Model.Vise_ViseEntiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model
{
    public class DobavljacDrveta
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }

        public ICollection<Nudi> Nudi { get; set; }
        public ICollection<SaradjujeSa> SaradjujeSa { get; set; }
    }
}
