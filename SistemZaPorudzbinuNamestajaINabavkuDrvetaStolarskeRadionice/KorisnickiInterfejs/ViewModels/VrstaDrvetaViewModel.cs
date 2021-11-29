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
    public class VrstaDrvetaViewModel : GenericCRUDViewModel<VrstaDrvetaModel>, ISecondLevel
    {
        public VrstaDrvetaViewModel()
        {
            Kolekcija = ListConverter<VrstaDrvetaModel>.KonvertujListu(VrstaDrvetaServis.Instance.CitajSve());
        }

        public void Dodaj()
        {
            Manager.ShowWindow(new VrstaDrvetaDodajViewModel());
            TryClose();
        }

        public void Obrisi()
        {
            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija brisanja uspesno izvrsila.";
                return;
            }

            VrstaDrvetaServis.Instance.Obrisi(int.Parse(Selektovano.Id));
            Kolekcija = ListConverter<VrstaDrvetaModel>.KonvertujListu(VrstaDrvetaServis.Instance.CitajSve());
        }

        public void Izmeni()
        {
            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija izmene uspesno izvrsila";
                return;
            }

            Manager.ShowWindow(new VrstaDrvetaDodajViewModel(Selektovano));
            TryClose();
        }
    }
}