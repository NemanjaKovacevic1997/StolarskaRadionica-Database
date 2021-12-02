using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Helpers
{
    public class OptionFull
    {
        public string Display { get; set; }
        public bool IsChecked { get; set; }
        public string Kolicina { get; set; }

        public OptionFull(string display)
        {
            Display = display;
            IsChecked = false;
            Kolicina = "";
        }
    }
}
