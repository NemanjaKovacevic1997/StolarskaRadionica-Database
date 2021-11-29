using BazaPodataka.Model.Vise_ViseEntiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model
{
    public class VrstaDrveta
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public ICollection<Skladisti> Skladisti { get; set; }

        public ICollection<Nudi> Nudi { get; set; }

        public ICollection<Sadrzi> Sadrzi { get; set; }
    }
}
