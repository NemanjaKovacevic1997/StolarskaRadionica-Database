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
    class CenovnikViewModel : GenericCRUDViewModel<CenovnikModel>, ISecondLevel
    {
        public CenovnikViewModel()
        {
            Kolekcija = ListConverter<CenovnikModel>.KonvertujListu(CenovnikServis.Instance.CitajSve());
        }

        public void Dodaj()
        {
            Manager.ShowWindow(new CenovnikDodajViewModel());
            TryClose();
        }

        public void Obrisi()
        {
            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija brisanja uspesno izvrsila.";
                return;
            }

            CenovnikServis.Instance.Obrisi(int.Parse(Selektovano.StolarskaRadionicaId));
            Kolekcija = ListConverter<CenovnikModel>.KonvertujListu(CenovnikServis.Instance.CitajSve());
        }

        public void Izmeni()
        {
            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija izmene uspesno izvrsila";
                return;
            }

            Manager.ShowWindow(new CenovnikDodajViewModel(Selektovano));
            TryClose();
        }
    }

} 

