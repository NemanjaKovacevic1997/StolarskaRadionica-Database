using KorisnickiInterfejs.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Models
{
    public class CenovnikModel : IEntityModel
    {
        public string StolarId { get; set; }
        public string StolarskaRadionicaId { get; set; }
    }
}
