using Caliburn.Micro;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using KorisnickiInterfejs.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.ViewModels
{
    public class StolarDodajViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private StolarModel modelUI = new StolarModel();
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

        public StolarDodajViewModel()
        {
            IsIzmena = false;
        }

        public StolarDodajViewModel(StolarModel modelUI)
        {
            this.modelUI = modelUI;
            Id = modelUI.Id;
            Ime = modelUI.Ime;
            Prezime = modelUI.Prezime;

            IsIzmena = true;

        }

       
        public string Id
        {
            get { return modelUI.Id; }
            set
            {
                modelUI.Id = value;
                if (!ValidacijaId(modelUI.Id))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Id");
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

        public bool CanDodaj(string id, string ime, string prezime)
        {
            if (ValidacijaId(id) &&
                ValidacijaIme(ime) &&
                ValidacijaPrezime(prezime))
                return true;
            return false;
        }

        public void Dodaj(string id, string ime, string prezime)
        {
            if (!IsIzmena)
                StolarServis.Instance.Dodaj(modelUI);
            else
                StolarServis.Instance.Izmeni(modelUI);

            Manager.ShowWindow(new StolarViewModel());
            TryClose();
        }

        public void Nazad()
        {
            Manager.ShowWindow(new StolarViewModel());
            TryClose();
        }

        #region VALIDACIJA
        private bool ValidacijaId(string str)
        {
            if (string.IsNullOrEmpty(str) || !int.TryParse(str, out int br))
                return false;

            if (br <= 0)
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
            if (!ValidacijaId(modelUI.Id))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Id");
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
