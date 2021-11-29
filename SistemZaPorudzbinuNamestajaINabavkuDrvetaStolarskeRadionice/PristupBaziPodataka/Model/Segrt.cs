using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BazaPodataka.Model
{
    [Table("Segrti")]
    public class Segrt : Radnik
    {
        public int? Ocena { get; set; }
    }
}
