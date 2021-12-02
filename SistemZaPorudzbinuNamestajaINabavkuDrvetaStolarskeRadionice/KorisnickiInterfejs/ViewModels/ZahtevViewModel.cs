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
    class ZahtevViewModel : GenericCRUDViewModel<ZahtevModel>, ISecondLevel
    {
        public ZahtevViewModel()
        {
            Kolekcija = ListConverter<ZahtevModel>.KonvertujListu(ZahtevServis.Instance.CitajSve());
        }

        public void Dodaj()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar, TipKorisnika.Magacioner}))
                return;

            Manager.ShowWindow(new ZahtevDodajViewModel());
            TryClose();
        }

        public void Obrisi()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar, TipKorisnika.Magacioner }))
                return;

            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija brisanja uspesno izvrsila.";
                return;
            }

            ZahtevServis.Instance.Obrisi(int.Parse(Selektovano.Id));
            Kolekcija = ListConverter<ZahtevModel>.KonvertujListu(ZahtevServis.Instance.CitajSve());
        }

        public void Izmeni()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar, TipKorisnika.Magacioner }))
                return;

            if (Selektovano == null)
            {
                ValidacionaGreska = "Morate selektovati odredjeni red da bi se operacija izmene uspesno izvrsila";
                return;
            }

            Manager.ShowWindow(new ZahtevDodajViewModel(Selektovano));
            TryClose();
        }
    }
}
