using Caliburn.Micro;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servis;
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
    public class StavkaPorudzbineDodajViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private StavkaPorudzbineModel modelUI = new StavkaPorudzbineModel();
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

        public StavkaPorudzbineDodajViewModel()
        {
            IsIzmena = false;
            Inicijalizacija();
        }

        public StavkaPorudzbineDodajViewModel(StavkaPorudzbineModel modelUI)
        {
            this.modelUI = modelUI;
            RedniBroj = modelUI.RedniBrojStavke;
            Kolicina = modelUI.Kolicina;
            IsIzmena = true;

            Inicijalizacija(modelUI.PorudzbinaMusterijaId + "+" + modelUI.PorudzbinaId, modelUI.RedniBrojStavkeCenovnika);
        }


        public string RedniBroj
        {
            get { return modelUI.RedniBrojStavke; }
            set
            {
                modelUI.RedniBrojStavke = value;
                if (!ValidacijaRedniBroj(modelUI.RedniBrojStavke))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("");
                else
                    ValidacijaSvega();
            }
        }

        public string Kolicina
        {
            get { return modelUI.Kolicina; }
            set
            {
                modelUI.Kolicina = value;
                if (!ValidacijaKolicina(modelUI.Kolicina))
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

        public bool CanDodaj(string redniBroj, string kolicina)
        {
            if (ValidacijaRedniBroj(redniBroj) &&
                ValidacijaKolicina(kolicina))
                return true;
            return false;
        }

        public void Dodaj(string redniBroj, string kolicina)
        {
            if (!IsIzmena)
            {
                var option1 = PronadjiOznacen(Options1);
                if (option1 == null)
                {
                    ValidacionaGreska = "Morate oznaciti jednu opciju za porudzbinu.";
                    return;
                }

                var option2 = PronadjiOznacen(Options2);
                if (option2 == null)
                {
                    ValidacionaGreska = "Morate oznaciti jednu opciju za stavku cenovnika.";
                    return;
                }

                string[] porudzbinaKljuc = option1.Display.Split('+');
                modelUI.PorudzbinaId = porudzbinaKljuc[1];
                modelUI.PorudzbinaMusterijaId = porudzbinaKljuc[0];
                string[] stavkaCenovnikaKljuc = option2.Display.Split('+');
                modelUI.RedniBrojStavkeCenovnika = stavkaCenovnikaKljuc[0];
                
                StavkaPorudzbineServis.Instance.Dodaj(modelUI);
            }
            else
                StavkaPorudzbineServis.Instance.Izmeni(modelUI);

            Manager.ShowWindow(new StavkaPorudzbineViewModel());
            TryClose();
        }

        public void Nazad()
        {
            Manager.ShowWindow(new StavkaPorudzbineViewModel());
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
            var ret1 = ListConverter<PorudzbinaModel>.KonvertujListu(PorudzbinaServis.Instance.CitajSve());
            foreach (var r1 in ret1)
            {
                Option o = new Option();
                o.Display = r1.MusterijaId + "+" + r1.Id.ToString();
                if (oznacen1 != "" && o.Display == oznacen1)
                    o.IsChecked = true;
                else
                    o.IsChecked = false;

                Options1.Add(o);
            }

            var ret2 = ListConverter<StavkaCenovnikaModel>.KonvertujListu(StavkaCenovnikaServis.Instance.CitajSve());
            foreach (var r2 in ret2)
            {
                Option o = new Option();
                o.Display = r2.RedniBrojStavke.ToString();
                if (oznacen2 != "" && o.Display == oznacen2)
                    o.IsChecked = true;
                else
                    o.IsChecked = false;

                Options2.Add(o);
            }
        }

        #region VALIDACIJA
        private bool ValidacijaRedniBroj(string str)
        {
            if (string.IsNullOrEmpty(str) || !int.TryParse(str, out int br))
                return false;

            if (br <= 0)
                return false;

            return true;
        }

        private bool ValidacijaKolicina(string str)
        {
            if (string.IsNullOrEmpty(str) || !int.TryParse(str, out int br))
                return false;

            if (br <= 0)
                return false;

            if (str.Length >= 7)
                return false;

            return true;
        }
        private void ValidacijaSvega()
        {
            if (!ValidacijaRedniBroj(modelUI.RedniBrojStavke))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("RedniBroj");
            if (!ValidacijaKolicina(modelUI.Kolicina))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Kolicina");
            else
                ValidacionaGreska = "";
        }
        #endregion
    }
}
