using BazaPodataka.Model.Vise_ViseEntiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model
{
    public class Zahtev
    {
        public int Id { get; set; }
        public DateTime DatumNastanka { get; set; }

        public ICollection<Sadrzi> Sadrzi { get; set; }

        public int MagacionerId { get; set; }
        public Magacioner Magacioner { get; set; }
    }
}
