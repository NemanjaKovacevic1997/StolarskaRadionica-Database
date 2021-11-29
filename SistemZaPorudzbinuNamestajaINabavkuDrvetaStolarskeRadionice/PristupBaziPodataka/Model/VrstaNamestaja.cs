using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BazaPodataka.Model
{
    public class VrstaNamestaja
    {
        public int Id { get; set; }

        [Index("IX_FirstAndSecond", 1, IsUnique = true)]
        public string Materijal { get; set; }


        [Index("IX_FirstAndSecond", 2, IsUnique = true)]
        public string Naziv { get; set; }

        public ICollection<StavkaCenovnika> StavkeCenovnika { get; set; }
    }
}
