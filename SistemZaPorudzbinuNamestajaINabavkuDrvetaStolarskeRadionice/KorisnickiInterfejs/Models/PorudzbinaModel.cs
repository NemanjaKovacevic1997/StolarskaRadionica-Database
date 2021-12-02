using KorisnickiInterfejs.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Models
{
    public class PorudzbinaModel : IEntityModel
    {
        public string Id { get; set; }
        public DateTime DatumPorudzbine { get; set; }
        public string Ukupno { get; set; }
        public string MusterijaId { get; set; }
    }
}
