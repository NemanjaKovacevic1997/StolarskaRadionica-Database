using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BazaPodataka.Model
{
    [Table("Stolari")]
    public class Stolar : Radnik
    {
        public ICollection<Cenovnik> Cenovnici { get; set; }
    }
}
