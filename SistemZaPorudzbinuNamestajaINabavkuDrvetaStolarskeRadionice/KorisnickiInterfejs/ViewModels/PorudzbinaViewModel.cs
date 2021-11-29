using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.ViewModels
{
    class PorudzbinaViewModel : GenericCRUDViewModel<PorudzbinaModel>, ISecondLevel
    {
        public PorudzbinaViewModel()
        {
            Kolekcija = ListConverter<PorudzbinaModel>.KonvertujListu(PorudzbinaServis.Instance.CitajSve());
        }

        public void Dodaj()
        {
            Manager.ShowWindow(new PorudzbinaDodajViewModel());
            TryClose();
        }

        public void Obrisi()
        {
            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija brisanja uspesno izvrsila.";
                return;
            }

            PorudzbinaServis.Instance.Obrisi(Selektovano.MusterijaId, int.Parse(Selektovano.Id));
            Kolekcija = ListConverter<PorudzbinaModel>.KonvertujListu(PorudzbinaServis.Instance.CitajSve());
        }

        public void Izmeni()
        {
            ValidacionaGreska = "Ovaj entitet nema polja koja bi mogla biti izmenjena.";
            return;
        }
    }
}
