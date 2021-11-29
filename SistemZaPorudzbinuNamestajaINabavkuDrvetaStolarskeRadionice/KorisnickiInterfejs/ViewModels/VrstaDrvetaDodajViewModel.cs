using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Caliburn.Micro;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;

namespace KorisnickiInterfejs.ViewModels
{
    public class VrstaDrvetaDodajViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private VrstaDrvetaModel modelUI = new VrstaDrvetaModel();
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

        public VrstaDrvetaDodajViewModel()
        {
            IsIzmena = false;
        }
        public VrstaDrvetaDodajViewModel(VrstaDrvetaModel modelUI)
        {
            this.modelUI = modelUI;
            Id = modelUI.Id;
            Naziv = modelUI.Naziv;

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

        public string Naziv
        {
            get { return modelUI.Naziv; }
            set
            {
                modelUI.Naziv = value;
                if (!ValidacijaNaziv(modelUI.Naziv))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("");
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

        public bool CanDodaj(string id, string naziv)
        {
            if (ValidacijaId(id) &&
                ValidacijaNaziv(naziv))
                return true;
            return false;
        }


        public void Dodaj(string id, string naziv)
        {
            if (!IsIzmena)
                VrstaDrvetaServis.Instance.Dodaj(modelUI);
            else
                VrstaDrvetaServis.Instance.Izmeni(modelUI);

            Manager.ShowWindow(new VrstaDrvetaViewModel());
            TryClose();
        }

        public void Nazad()
        {
            Manager.ShowWindow(new VrstaDrvetaViewModel());
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
            else if (!ValidacijaNaziv(modelUI.Naziv))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Naziv");
            else
                ValidacionaGreska = "";
        }
        #endregion
    }
}