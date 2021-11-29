using Caliburn.Micro;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.ViewModels
{
    public class MusterijaViewModel : GenericCRUDViewModel<MusterijaModel>, ISecondLevel
    {
        public MusterijaViewModel()
        {
            Kolekcija = ListConverter<MusterijaModel>.KonvertujListu(MusterijaServis.Instance.CitajSve());
        }

        public void Dodaj()
        {
            Manager.ShowWindow(new MusterijaDodajViewModel());
            TryClose();
        }

        public void Obrisi()
        {
            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija brisanja uspesno izvrsila.";
                return;
            }

            MusterijaServis.Instance.Obrisi(Selektovano.JMBG);
            Kolekcija = ListConverter<MusterijaModel>.KonvertujListu(MusterijaServis.Instance.CitajSve());
        }

        public void Izmeni()
        {
            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija izmene uspesno izvrsila";
                return;
            }

            Manager.ShowWindow(new MusterijaDodajViewModel(Selektovano));
            TryClose();
        }




    }
}
