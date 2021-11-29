using Caliburn.Micro;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KorisnickiInterfejs.ViewModels
{
    public class AuthenticateViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private AuthenticateModel modelUI = new AuthenticateModel();
        public string KorisnickoIme
        {
            get { return modelUI.KorisnickoIme; }
            set
            {
                modelUI.KorisnickoIme = value;
            }
        }

        public string Lozinka
        {
            get { return modelUI.Lozinka; }
            set
            {
                modelUI.Lozinka = value;
            }
        }

        private string _validacionaGreska = "";

        public string ValidacionaGreska
        {
            get { return _validacionaGreska; }
            set
            {
                _validacionaGreska = value;
                NotifyOfPropertyChange(() => ValidacionaGreska);
            }
        }


        public bool CanPrijava(string korisnickoIme, string lozinka)
        {
            return true;
        }

        public void Prijava(string korisnickoIme, string lozinka)
        {
            var user = AuthenticateService.Instance.Login(korisnickoIme, lozinka);
            if (user == null)
            {
                MessageBox.Show("Netacni kredencijali");
                return;
            }
            Manager.ShowWindow(new PocetniViewModel());
            TryClose();
        }

        public void Izlaz() => TryClose();
    }
}
