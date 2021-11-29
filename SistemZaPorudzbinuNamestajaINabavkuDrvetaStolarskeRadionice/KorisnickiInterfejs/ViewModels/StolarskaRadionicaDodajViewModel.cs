using BazaPodataka.Model.Vise_ViseEntiteti;
using Caliburn.Micro;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using KorisnickiInterfejs.Views;
using PristupBaziPodataka;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace KorisnickiInterfejs.ViewModels
{
    public class StolarskaRadionicaDodajViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private StolarskaRadionicaModel _stolarskaRadionica = new StolarskaRadionicaModel();
        private FabrikaPoruka _fabrikaPoruka = new FabrikaPoruka();
        private bool _isIzmena;
        private Repository<SaradjujeSa> repo = new Repository<SaradjujeSa>();
        private IEnumerable<SaradjujeSa> kolekcijaOdabranih;

        private ObservableCollection<Option> _options;
        public ObservableCollection<Option> Options
        {
            get { return _options; }
            set
            {
                _options = value;
                NotifyOfPropertyChange(() => Options);
            }
        }



        public bool IsIzmena
        {
            get { return _isIzmena; }
            set 
            {
                _isIzmena = value;
                NotifyOfPropertyChange(() => IsIzmena);
            }
        }

        public StolarskaRadionicaDodajViewModel()
        {
            IsIzmena = false;
            Options = new ObservableCollection<Option>();
            var ret = ListConverter<DobavljacDrvetaModel>.KonvertujListu(DobavljacDrvetaServis.Instance.CitajSve());
            foreach (var r in ret)
            {
                Option o = new Option();
                o.Display = r.Id;
                Options.Add(o);
            }
        }
        public StolarskaRadionicaDodajViewModel(StolarskaRadionicaModel modelUI)
        {
            _stolarskaRadionica = modelUI;
            Id = modelUI.Id;
            Naziv = modelUI.Naziv;
            Mesto = modelUI.Mesto;
            Ulica = modelUI.Ulica;
            Broj = modelUI.Broj;
            IsIzmena = true;

            Options = new ObservableCollection<Option>();
            var ret2 = ListConverter<DobavljacDrvetaModel>.KonvertujListu(DobavljacDrvetaServis.Instance.CitajSve());

            kolekcijaOdabranih = repo.GetAll().Where(i => i.StolarskaRadionicaId.ToString() == modelUI.Id);

            foreach (var vd in ret2)
            {
                Option o = new Option();
                o.Display = vd.Id;
                o.IsChecked = false;
                foreach (var odabrani in kolekcijaOdabranih)
                {
                    if (odabrani.DobavljacDrvetaId.ToString() == vd.Id && odabrani.StolarskaRadionicaId.ToString() == modelUI.Id)
                        o.IsChecked = true;
                }

                Options.Add(o);
            }
        }

        public string Id
        {
            get { return _stolarskaRadionica.Id; }
            set
            {
                _stolarskaRadionica.Id = value;
                if (!ValidacijaId(_stolarskaRadionica.Id))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Id");
                else
                    ValidacijaSvega();
            }
        }

        public string Naziv
        {
            get { return _stolarskaRadionica.Naziv; }
            set 
            { 
                _stolarskaRadionica.Naziv = value;
                if (!ValidacijaNaziv(_stolarskaRadionica.Naziv))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Naziv");
                else
                    ValidacijaSvega();
            }
        }

        public string Mesto
        {
            get { return _stolarskaRadionica.Mesto; }
            set 
            {
                _stolarskaRadionica.Mesto = value;
                if (!ValidacijaMesto(_stolarskaRadionica.Mesto))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Mesto");
                else
                    ValidacijaSvega();
            }
        }

        public string Ulica
        {
            get { return _stolarskaRadionica.Ulica; }
            set 
            {
                _stolarskaRadionica.Ulica = value;
                if (!ValidacijaUlica(_stolarskaRadionica.Ulica))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Ulica");
                else
                    ValidacijaSvega();
            }
        }

        public string Broj
        {
            get { return _stolarskaRadionica.Broj; }
            set 
            { 
                _stolarskaRadionica.Broj = value;
                if (!ValidacijaBroj(_stolarskaRadionica.Broj))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Broj");
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

        public bool CanDodaj(string id, string naziv, string mesto, string broj, string ulica)
        {
            if (ValidacijaId(id) &&
                ValidacijaNaziv(naziv) &&
                ValidacijaMesto(mesto) &&
                ValidacijaBroj(broj) &&
                ValidacijaUlica(ulica))
                return true;
            return false;
        }
        

        public void Dodaj(string id, string naziv, string mesto, string broj, string ulica)
        {
            if (!IsIzmena)
            {
                StolarskaRadionicaServis.Instance.Dodaj(_stolarskaRadionica);

                var options = PronadjiOznacene(Options);
                foreach (var o in options)
                {
                    repo.AddOrUpdate(new SaradjujeSa()
                    {
                        StolarskaRadionicaId = int.Parse(_stolarskaRadionica.Id),
                        DobavljacDrvetaId = int.Parse(o.Display)
                    });
                }
            }
            else
            {
                var options = PronadjiOznacene(Options);
  
                StolarskaRadionicaServis.Instance.Izmeni(_stolarskaRadionica);

                foreach (var s in kolekcijaOdabranih)
                    repo.Remove(new object[] { s.DobavljacDrvetaId, s.StolarskaRadionicaId });

                foreach (var o in options)
                {
                    repo.AddOrUpdate(new SaradjujeSa()
                    {
                        StolarskaRadionicaId = int.Parse(_stolarskaRadionica.Id),
                        DobavljacDrvetaId = int.Parse(o.Display)
                    });
                }
            }
                

            Manager.ShowWindow(new StolarskaRadionicaViewModel());
            TryClose();
        }

        public void Nazad()
        {
            Manager.ShowWindow(new StolarskaRadionicaViewModel());
            TryClose();
        }

        private List<Option> PronadjiOznacene(ObservableCollection<Option> opcije)
        {
            List<Option> ret = new List<Option>();
            foreach (var o in opcije)
            {
                if (o.IsChecked)
                    ret.Add(o);
            }

            return ret;
        }

        #region VALIDACIJA
        private bool ValidacijaId(string str)
        {
            if (string.IsNullOrEmpty(str) || !int.TryParse(str, out int number))
                return false;

            if (number <= 0)
                return false;

            return true;
        }

        private bool ValidacijaNaziv(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            if (str.Length >= 30)
                return false;

            return true;
        }

        private bool ValidacijaMesto(string str)
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

        private bool ValidacijaUlica(string str)
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

        private bool ValidacijaBroj(string str)
        {
            if (!int.TryParse(str, out int number))
                return false;

            if (number <= 0)
                return false;

            if (str.Length >= 5)
                return false;

            return true;
        }

        private void ValidacijaSvega()
        {
            if (!ValidacijaId(_stolarskaRadionica.Id))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Id");
            else if (!ValidacijaNaziv(_stolarskaRadionica.Naziv))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Naziv");
            else if (!ValidacijaMesto(_stolarskaRadionica.Mesto))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Mesto");
            else if (!ValidacijaUlica(_stolarskaRadionica.Ulica))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Ulica");
            else if (!ValidacijaBroj(_stolarskaRadionica.Broj))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Broj");
            else
                ValidacionaGreska = "";
        }
        #endregion
    }
}
