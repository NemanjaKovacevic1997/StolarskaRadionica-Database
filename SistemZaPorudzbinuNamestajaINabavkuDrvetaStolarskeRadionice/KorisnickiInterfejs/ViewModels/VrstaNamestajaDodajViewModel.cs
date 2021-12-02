using Caliburn.Micro;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.ViewModels
{
    public class VrstaNamestajaDodajViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private VrstaNamestajaModel modelUI = new VrstaNamestajaModel();
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

        public VrstaNamestajaDodajViewModel()
        {
            IsIzmena = false;
        }
        public VrstaNamestajaDodajViewModel(VrstaNamestajaModel modelUI)
        {
            this.modelUI = modelUI;
            Id = modelUI.Id;
            Naziv = modelUI.Naziv;
            Materijal = modelUI.Materijal;

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

        public string Materijal
        {
            get { return modelUI.Materijal; }
            set
            {
                modelUI.Materijal = value;
                if (!ValidacijaMaterijal(modelUI.Materijal))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Materijal");
                else
                    ValidacijaSvega();
            }
        }

        public string Naziv
        {
            get { return modelUI.Naziv; }
            set
            {
                modelUI.Naziv = value;
                if (!ValidacijaNaziv(modelUI.Naziv))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Naziv");
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

        public bool CanDodaj(string id, string materijal, string naziv)
        {
            if (ValidacijaId(id) &&
                ValidacijaMaterijal(materijal) &&
                ValidacijaNaziv(naziv))
                return true;
            return false;
        }


        public void Dodaj(string id, string materijal, string naziv)
        {
            if (!IsIzmena)
                VrstaNamestajaServis.Instance.Dodaj(modelUI);
            else
                VrstaNamestajaServis.Instance.Izmeni(modelUI);

            Manager.ShowWindow(new VrstaNamestajaViewModel());
            TryClose();
        }

        public void Nazad()
        {
            Manager.ShowWindow(new VrstaNamestajaViewModel());
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

        private bool ValidacijaMaterijal(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            string str2 = Regex.Replace(str, @"\s+", "");
            if (!str2.All(char.IsLetter))
                return false;

            if (str.Length >= 30)
                return false;

            return true;
        }

        
        private bool ValidacijaNaziv(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            string str2 = Regex.Replace(str, @"\s+", "");
            if (!str2.All(char.IsLetter))
                return false;

            if (str.Length >= 30)
                return false;

            return true;
        }


        private void ValidacijaSvega()
        {
            if (!ValidacijaId(modelUI.Id))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Id");
            else if (!ValidacijaMaterijal(modelUI.Materijal))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Materijal");
            else if (!ValidacijaNaziv(modelUI.Naziv))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Naziv");
            else
                ValidacionaGreska = "";
        }
        #endregion
    }
}
