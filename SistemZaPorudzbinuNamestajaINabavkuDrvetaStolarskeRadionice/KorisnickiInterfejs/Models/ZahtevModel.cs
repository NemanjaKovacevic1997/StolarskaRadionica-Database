using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Models
{
    public class ZahtevModel
    {
        public string Id { get; set; }
        public DateTime DatumNastanka { get; set; }
        public int MagacionerId { get; set; }
    }
}
