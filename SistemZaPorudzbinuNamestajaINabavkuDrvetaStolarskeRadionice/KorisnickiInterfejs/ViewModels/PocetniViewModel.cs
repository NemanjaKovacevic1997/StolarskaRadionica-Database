using Caliburn.Micro;
using KorisnickiInterfejs.Servisi;
using KorisnickiInterfejs.Servisi.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KorisnickiInterfejs.ViewModels
{
    public class PocetniViewModel : Screen
    {
        IWindowManager manager = new WindowManager();

        public void PrikaziRegistracija()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin }))
                return;

            manager.ShowWindow(new RegisterViewModel());
        }

        public void PrikaziMusterija()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar, TipKorisnika.Segrt}))
                return;

            manager.ShowWindow(new MusterijaViewModel());
        }

        public void PrikaziPoruzbina()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar, TipKorisnika.Segrt }))
                return;

            manager.ShowWindow(new PorudzbinaViewModel());
        }

        public void PrikaziStavkaPoruzbine()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar, TipKorisnika.Segrt }))
                return;

            manager.ShowWindow(new StavkaPorudzbineViewModel());
        }


        public void PrikaziStavkaCenovnika()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar, TipKorisnika.Segrt }))
                return;

            manager.ShowWindow(new StavkaCenovnikaViewModel());
        }

        public void PrikaziVrstaNamestaja()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar, TipKorisnika.Segrt }))
                return;

            manager.ShowWindow(new VrstaNamestajaViewModel());           
        }

        public void PrikaziSegrt()
        {
            manager.ShowWindow(new SegrtViewModel());
        }

        public void PrikaziMacioner()
        {
            manager.ShowWindow(new MagacionerViewModel());
        }

        public void PrikaziStolar()
        {
            manager.ShowWindow(new StolarViewModel());
        }

        public void PrikaziZahtev()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar, TipKorisnika.Magacioner }))
                return;

            manager.ShowWindow(new ZahtevViewModel());
        }

        public void PrikaziVrstaDrveta()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar, TipKorisnika.Magacioner }))
                return;

            manager.ShowWindow(new VrstaDrvetaViewModel());
        }

        public void PrikaziDobavljacDrveta()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar, TipKorisnika.Magacioner }))
                return;

            manager.ShowWindow(new DobavljacDrvetaViewModel());
        }

        public void PrikaziMagacin()
        {
            if (!AuthorizationService.IsAuthorized(new List<TipKorisnika>() { TipKorisnika.Admin, TipKorisnika.Stolar, TipKorisnika.Magacioner }))
                return;

            manager.ShowWindow(new MagacinViewModel());
        }

        public void PrikaziFunkcije()
        {
            manager.ShowWindow(new FunkcijeViewModel());
        }
    }
}
