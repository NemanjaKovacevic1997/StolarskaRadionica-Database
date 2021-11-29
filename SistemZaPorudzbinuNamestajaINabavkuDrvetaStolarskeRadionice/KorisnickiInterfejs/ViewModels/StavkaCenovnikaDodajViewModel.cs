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
    public class StavkaCenovnikaDodajViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private StavkaCenovnikaModel modelUI = new StavkaCenovnikaModel();
        private FabrikaPoruka _fabrikaPoruka = new FabrikaPoruka();
        private ObservableCollection<Option> _options1;
        private ObservableCollection<Option> _options2;
        public ObservableCollection<Option> Options1
        {
            get { return _options1; }
            set
            {
                _options1 = value;
                NotifyOfPropertyChange(() => Options1);
            }
        }
        public ObservableCollection<Option> Options2
        {
            get { return _options2; }
            set
            {
                _options2 = value;
                NotifyOfPropertyChange(() => Options2);
            }
        }

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

        public StavkaCenovnikaDodajViewModel()
        {
            IsIzmena = false;
            Inicijalizacija();
        }

        public StavkaCenovnikaDodajViewModel(StavkaCenovnikaModel modelUI)
        {
            this.modelUI = modelUI;
            RedniBroj = modelUI.RedniBrojStavke;
            Cena = modelUI.Cena;
            IsIzmena = true;

            Inicijalizacija(modelUI.CenovnikId, modelUI.VrstaNamestajaId);
        }


        public string RedniBroj
        {
            get { return modelUI.RedniBrojStavke; }
            set
            {
                modelUI.RedniBrojStavke = value;
                if (!ValidacijaRedniBroj(modelUI.RedniBrojStavke))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("RedniBroj");
                else
                    ValidacijaSvega();
            }
        }

        public string Cena
        {
            get { return modelUI.Cena; }
            set
            {
                modelUI.Cena = value;
                if (!ValidacijaRedniBroj(modelUI.Cena))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Cena");
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

        public bool CanDodaj(string redniBroj, string cena)
        {
            if (ValidacijaRedniBroj(redniBroj) &&
                ValidacijaCena(cena))
                return true;
            return false;
        }

        public void Dodaj(string redniBroj, string cena)
        {
            if (!IsIzmena)
            {
                var option1 = PronadjiOznacen(Options1);
                if (option1 == null)
                {
                    ValidacionaGreska = "Morate oznaciti jednu opciju za cenovnik.";
                    return;
                }

                var option2 = PronadjiOznacen(Options2);
                if (option2 == null)
                {
                    ValidacionaGreska = "Morate oznaciti jednu opciju za vrstu namestaja.";
                    return;
                }

                modelUI.CenovnikId = option1.Display;
                modelUI.VrstaNamestajaId = option2.Display;
                StavkaCenovnikaServis.Instance.Dodaj(modelUI);
            }
            else
                StavkaCenovnikaServis.Instance.Izmeni(modelUI);

            Manager.ShowWindow(new StavkaCenovnikaViewModel());
            TryClose();
        }

        public void Nazad()
        {
            Manager.ShowWindow(new StavkaCenovnikaViewModel());
            TryClose();
        }

        private Option PronadjiOznacen(ObservableCollection<Option> opcije)
        {
            foreach (var o in opcije)
            {
                if (o.IsChecked)
                    return o;
            }

            return null;
        }

        private void Inicijalizacija(string oznacen1 = "", string oznacen2 = "")
        {
            Options1 = new ObservableCollection<Option>();
            Options2 = new ObservableCollection<Option>();
            var ret1 = ListConverter<CenovnikModel>.KonvertujListu(CenovnikServis.Instance.CitajSve());
            foreach (var r1 in ret1)
            {
                Option o = new Option();
                o.Display = r1.StolarskaRadionicaId;
                if (oznacen1 != "" && o.Display == oznacen1)
                    o.IsChecked = true;
                else
                    o.IsChecked = false;

                Options1.Add(o);
            }

            var ret2 = ListConverter<VrstaNamestajaModel>.KonvertujListu(VrstaNamestajaServis.Instance.CitajSve());
            foreach (var r2 in ret2)
            {
                Option o = new Option();
                o.Display = r2.Id;
                if (oznacen2 != "" && o.Display == oznacen2)
                     o.IsChecked = true;
                 else
                     o.IsChecked = false;

                Options2.Add(o);
            }
        }

        #region VALIDACIJA
        private bool ValidacijaCena(string str)
        {
            if (string.IsNullOrEmpty(str) || !double.TryParse(str, out double ret))
                return false;

            if (ret <= 0)
                return false;

            if (str.Length >= 10)
                return false;

            return true;
        }

        private bool ValidacijaRedniBroj(string str)
        {
            if (string.IsNullOrEmpty(str) || !int.TryParse(str, out int number))
                return false;

            if (number <= 0)
                return false;

            return true;
        }
        private void ValidacijaSvega()
        {
            if (!ValidacijaRedniBroj(modelUI.RedniBrojStavke))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("RedniBroj");
            if (!ValidacijaCena(modelUI.Cena))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Cena");
            else
                ValidacionaGreska = "";
        }
        #endregion
    }
}
