using BazaPodataka.Model.Vise_ViseEntiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace BazaPodataka.Model
{
    public class Magacin
    {
        public int Id { get; set; }

        public ICollection<Skladisti> Skladisti { get; set; }
    }
}
