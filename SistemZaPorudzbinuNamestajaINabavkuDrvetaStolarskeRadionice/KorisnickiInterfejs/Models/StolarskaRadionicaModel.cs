using KorisnickiInterfejs.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Models
{
    public class StolarskaRadionicaModel : IEntityModel
    {
        public string Id { get; set; }
        public string Naziv { get; set; }
        public string Mesto { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }

        public StolarskaRadionicaModel(string id = "", string naziv = "", string mesto = "", string ulica = "", string broj = "")
        {
            Id = id;
            Naziv = naziv;
            Mesto = mesto;
            Ulica = ulica;
            Broj = broj;
        }
    }
}
