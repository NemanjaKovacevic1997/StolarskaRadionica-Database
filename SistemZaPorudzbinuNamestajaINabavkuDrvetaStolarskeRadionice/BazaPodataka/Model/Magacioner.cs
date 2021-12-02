using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BazaPodataka.Model
{
    [Table("Magacioneri")]
    public class Magacioner : Radnik
    {
        public ICollection<Zahtev> Zahtevi { get; set; }
    }
}
