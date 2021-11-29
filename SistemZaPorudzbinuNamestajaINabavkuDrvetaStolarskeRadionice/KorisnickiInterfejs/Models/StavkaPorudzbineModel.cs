using KorisnickiInterfejs.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Models
{
    public class StavkaPorudzbineModel : IEntityModel
    {
        public string RedniBrojStavke { get; set; }
        public string Kolicina { get; set; }

        public string PorudzbinaMusterijaId { get; set; }
        public string PorudzbinaId { get; set; }

        public string StavkaCenovnikaId { get; set; }
        public string RedniBrojStavkeCenovnika { get; set; }
    }
}
