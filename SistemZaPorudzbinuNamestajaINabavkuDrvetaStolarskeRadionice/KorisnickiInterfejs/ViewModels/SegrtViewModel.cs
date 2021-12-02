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
    public class SegrtViewModel : GenericCRUDViewModel<SegrtModel>, ISecondLevel
    {
        public SegrtViewModel()
        {
            Kolekcija = ListConverter<SegrtModel>.KonvertujListu(SegrtServis.Instance.CitajSve());
        }

        public void Dodaj()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin }))
                return;

            Manager.ShowWindow(new SegrtDodajViewModel());
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

            SegrtServis.Instance.Obrisi(int.Parse(Selektovano.Id));
            Kolekcija = ListConverter<SegrtModel>.KonvertujListu(SegrtServis.Instance.CitajSve());
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

            Manager.ShowWindow(new SegrtDodajViewModel(Selektovano));
            TryClose();
        }
    }
}
