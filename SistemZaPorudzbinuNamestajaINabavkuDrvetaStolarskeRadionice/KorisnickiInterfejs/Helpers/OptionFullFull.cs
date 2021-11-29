using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Helpers
{
    class OptionFullFull
    {
        public string Display { get; set; }
        public bool IsChecked { get; set; }
        public string Kolicina { get; set; }
        public string Cena { get; set; }

        public OptionFullFull(string display)
        {
            Display = display;
            IsChecked = false;
            Kolicina = "";
            Cena = "";
        }
    }
}
