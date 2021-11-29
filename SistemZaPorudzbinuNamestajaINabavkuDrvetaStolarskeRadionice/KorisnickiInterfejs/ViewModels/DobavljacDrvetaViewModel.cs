using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;

namespace KorisnickiInterfejs.ViewModels
{
    class DobavljacDrvetaViewModel : GenericCRUDViewModel<DobavljacDrvetaModel>, ISecondLevel
    {
        public DobavljacDrvetaViewModel()
        {
            Kolekcija = ListConverter<DobavljacDrvetaModel>.KonvertujListu(DobavljacDrvetaServis.Instance.CitajSve());
        }

        public void Dodaj()
        {
            Manager.ShowWindow(new DobavljacDrvetaDodajViewModel());
            TryClose();
        }

        public void Obrisi()
        {
            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija brisanja uspesno izvrsila.";
                return;
            }

            DobavljacDrvetaServis.Instance.Obrisi(int.Parse(Selektovano.Id));
            Kolekcija = ListConverter<DobavljacDrvetaModel>.KonvertujListu(DobavljacDrvetaServis.Instance.CitajSve());
        }

        public void Izmeni()
        {
            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija izmene uspesno izvrsila";
                return;
            }

            Manager.ShowWindow(new DobavljacDrvetaDodajViewModel(Selektovano));
            TryClose();
        }
    }
}