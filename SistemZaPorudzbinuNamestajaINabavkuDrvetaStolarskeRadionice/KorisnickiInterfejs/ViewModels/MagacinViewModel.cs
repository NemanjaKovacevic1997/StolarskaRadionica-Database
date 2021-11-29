using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using KorisnickiInterfejs.Views;

namespace KorisnickiInterfejs.ViewModels
{
    public class MagacinViewModel : GenericCRUDViewModel<MagacinModel>, ISecondLevel
    {
        public MagacinViewModel()
        {
            Kolekcija = ListConverter<MagacinModel>.KonvertujListu(MagacinServis.Instance.CitajSve());
        }

        public void Dodaj()
        {
            Manager.ShowWindow(new MagacinDodajViewModel());
            TryClose();
        }

        public void Obrisi()
        {
            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija brisanja uspesno izvrsila.";
                return;
            }

            MagacinServis.Instance.Obrisi(int.Parse(Selektovano.StolarskaRadionicaId));
            Kolekcija = ListConverter<MagacinModel>.KonvertujListu(MagacinServis.Instance.CitajSve());
        }

        public void Izmeni()
        {
            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija izmene uspesno izvrsila";
                return;
            }

            Manager.ShowWindow(new MagacinDodajViewModel(Selektovano));
            TryClose();
        }
    }

}