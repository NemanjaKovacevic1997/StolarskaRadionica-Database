using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servis;
using KorisnickiInterfejs.Servisi;
using KorisnickiInterfejs.Servisi.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.ViewModels
{
    public class StavkaPorudzbineViewModel : GenericCRUDViewModel<StavkaPorudzbineModel>, ISecondLevel
    {
        public StavkaPorudzbineViewModel()
        {
            Kolekcija = ListConverter<StavkaPorudzbineModel>.KonvertujListu(StavkaPorudzbineServis.Instance.CitajSve());
        }

        public void Dodaj()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar }))
                return;

            Manager.ShowWindow(new StavkaPorudzbineDodajViewModel());
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
            
            StavkaPorudzbineServis.Instance.Obrisi(int.Parse(Selektovano.PorudzbinaId), Selektovano.PorudzbinaMusterijaId, int.Parse(Selektovano.RedniBrojStavke));
            Kolekcija = ListConverter<StavkaPorudzbineModel>.KonvertujListu(StavkaPorudzbineServis.Instance.CitajSve());
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

            Manager.ShowWindow(new StavkaPorudzbineDodajViewModel(Selektovano));
            TryClose();
        }
    }
}
