using BazaPodataka.Model.Vise_ViseEntiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model
{
    public class StolarskaRadionica
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public Cenovnik Cenovnik { get; set; }

        public ICollection<Radnik> Radnici { get; set; }

        public Magacin Magacin { get; set; }

        public ICollection<SaradjujeSa> SaradjujeSa { get; set; }
    }
}
