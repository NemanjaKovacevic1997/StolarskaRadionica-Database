using Caliburn.Micro;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using KorisnickiInterfejs.Servisi.Auth;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KorisnickiInterfejs.ViewModels
{
    public class StolarskaRadionicaViewModel : GenericCRUDViewModel<StolarskaRadionicaModel>, ISecondLevel
    {
        public StolarskaRadionicaViewModel() : base()
        {
            AuthorizationService.Authorize(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar });
            Kolekcija = ListConverter<StolarskaRadionicaModel>.KonvertujListu(StolarskaRadionicaServis.Instance.CitajSve());
        }

       
        public void Dodaj()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin }))
                return;

            Manager.ShowWindow(new StolarskaRadionicaDodajViewModel());
            TryClose();
        }

        public void Obrisi()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin }))
                return;

            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija brisanja uspesno izvrsila.";
                return;
            }

            StolarskaRadionicaServis.Instance.Obrisi(Selektovano.Id);
            Kolekcija = ListConverter<StolarskaRadionicaModel>.KonvertujListu(StolarskaRadionicaServis.Instance.CitajSve());
        }

        public void Izmeni()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin }))
                return;

            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija izmene uspesno izvrsila";
                return;
            }

            Manager.ShowWindow(new StolarskaRadionicaDodajViewModel(Selektovano));
            TryClose();
        }
       
    }
}
