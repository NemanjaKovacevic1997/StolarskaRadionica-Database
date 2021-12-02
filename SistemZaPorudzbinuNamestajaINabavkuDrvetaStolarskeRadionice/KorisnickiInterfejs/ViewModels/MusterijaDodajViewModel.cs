using Caliburn.Micro;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.ViewModels
{
    public class MusterijaDodajViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private MusterijaModel modelUI = new MusterijaModel();
        private FabrikaPoruka _fabrikaPoruka = new FabrikaPoruka();

        private bool _isIzmena;
        public bool IsIzmena
        {
            get { return _isIzmena; }
            set
            {
                _isIzmena = value;
                NotifyOfPropertyChange(() => IsIzmena);
            }
        }

        public MusterijaDodajViewModel()
        {
            IsIzmena = false;
        }
        public MusterijaDodajViewModel(MusterijaModel modelUI)
        {
            this.modelUI = modelUI;
            Ime = modelUI.Ime;
            Prezime = modelUI.Prezime;
            Jmbg = modelUI.JMBG;
            
            IsIzmena = true;
        }


        public string Jmbg
        {
            get { return modelUI.JMBG; }
            set
            {
                modelUI.JMBG = value;
                if (!ValidacijaJmbg(modelUI.JMBG))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Jmbg");
                else
                    ValidacijaSvega();
            }
        }

        public string Ime
        {
            get { return modelUI.Ime; }
            set
            {
                modelUI.Ime = value;
                if (!ValidacijaIme(modelUI.Ime))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Ime");
                else
                    ValidacijaSvega();
            }
        }

        public string Prezime
        {
            get { return modelUI.Prezime; }
            set
            {
                modelUI.Prezime = value;
                if (!ValidacijaPrezime(modelUI.Prezime))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Prezime");
                else
                    ValidacijaSvega();
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

        public bool CanDodaj(string jmbg, string ime, string prezime)
        {
            if (ValidacijaJmbg(jmbg) &&
                ValidacijaIme(ime) &&
                ValidacijaPrezime(prezime))
                return true;
            return false;
        }


        public void Dodaj(string jmbg, string ime, string prezime)
        {
            if (!IsIzmena)
                MusterijaServis.Instance.Dodaj(modelUI);
            else
                MusterijaServis.Instance.Izmeni(modelUI);

            Manager.ShowWindow(new MusterijaViewModel());
            TryClose();
        }

        public void Nazad()
        {
            Manager.ShowWindow(new MusterijaViewModel());
            TryClose();
        }

        #region VALIDACIJA
        private bool ValidacijaJmbg(string str)
        {
            if (!str.All(char.IsDigit))
                return false;

            if (str.Length != 13)
                return false;

            return true;
        }

        private bool ValidacijaIme(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || !str.All(char.IsLetter))
                return false;

            if (str.Length >= 30)
                return false;

            return true;
        }

        private bool ValidacijaPrezime(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || !str.All(char.IsLetter))
                return false;

            if (str.Length >= 30)
                return false;

            return true;
        }

    
        private void ValidacijaSvega()
        {
            if (!ValidacijaJmbg(modelUI.JMBG))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Jmbg");
            else if (!ValidacijaIme(modelUI.Ime))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Ime");
            else if (!ValidacijaPrezime(modelUI.Prezime))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Prezime");
            else
                ValidacionaGreska = "";
        }
        #endregion
    }
}
