using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using KorisnickiInterfejs.Servisi.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.ViewModels
{
    public class VrstaNamestajaViewModel : GenericCRUDViewModel<VrstaNamestajaModel>, ISecondLevel
    {
        public VrstaNamestajaViewModel()
        {
            Kolekcija = ListConverter<VrstaNamestajaModel>.KonvertujListu(VrstaNamestajaServis.Instance.CitajSve());
        }

        public void Dodaj()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar }))
                return;

            Manager.ShowWindow(new VrstaNamestajaDodajViewModel());
            TryClose();
        }

        public void Obrisi()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar }))
                return;

            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija brisanja uspesno izvrsila.";
                return;
            }

            VrstaNamestajaServis.Instance.Obrisi(int.Parse(Selektovano.Id));
            Kolekcija = ListConverter<VrstaNamestajaModel>.KonvertujListu(VrstaNamestajaServis.Instance.CitajSve());
        }

        public void Izmeni()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar }))
                return;

            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija izmene uspesno izvrsila";
                return;
            }

            Manager.ShowWindow(new VrstaNamestajaDodajViewModel(Selektovano));
            TryClose();
        }
    }
}
