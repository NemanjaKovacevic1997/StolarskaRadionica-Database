using System.Collections.Generic;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using KorisnickiInterfejs.Servisi.Auth;


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
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar }))
                return;

            Manager.ShowWindow(new MagacinDodajViewModel());
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

            MagacinServis.Instance.Obrisi(int.Parse(Selektovano.Id));
            Kolekcija = ListConverter<MagacinModel>.KonvertujListu(MagacinServis.Instance.CitajSve());
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

            Manager.ShowWindow(new MagacinDodajViewModel(Selektovano));
            TryClose();
        }
    }

}