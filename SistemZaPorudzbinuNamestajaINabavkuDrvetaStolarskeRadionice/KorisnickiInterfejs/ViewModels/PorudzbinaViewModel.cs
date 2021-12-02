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
    class PorudzbinaViewModel : GenericCRUDViewModel<PorudzbinaModel>, ISecondLevel
    {
        public PorudzbinaViewModel()
        {
            Kolekcija = ListConverter<PorudzbinaModel>.KonvertujListu(PorudzbinaServis.Instance.CitajSve());
        }

        public void Dodaj()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar }))
                return;

            Manager.ShowWindow(new PorudzbinaDodajViewModel());
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

            PorudzbinaServis.Instance.Obrisi(Selektovano.MusterijaId, int.Parse(Selektovano.Id));
            Kolekcija = ListConverter<PorudzbinaModel>.KonvertujListu(PorudzbinaServis.Instance.CitajSve());
        }

        public void Izmeni()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar }))
                return;

            ValidacionaGreska = "Ovaj entitet nema polja koja bi mogla biti izmenjena.";
            return;
        }
    }
}
