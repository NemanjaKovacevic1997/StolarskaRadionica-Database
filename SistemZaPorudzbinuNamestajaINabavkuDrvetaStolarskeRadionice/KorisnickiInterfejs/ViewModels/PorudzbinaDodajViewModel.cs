using Caliburn.Micro;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using KorisnickiInterfejs.Servisi.Auth;
using KorisnickiInterfejs.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.ViewModels
{
    public class PorudzbinaDodajViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private PorudzbinaModel modelUI = new PorudzbinaModel();
        private FabrikaPoruka _fabrikaPoruka = new FabrikaPoruka();
        private ObservableCollection<Option> _options1;
        public ObservableCollection<Option> Options1
        {
            get { return _options1; }
            set
            {
                _options1 = value;
                NotifyOfPropertyChange(() => Options1);
            }
        }

        public Option SelectedOption1 { get; set; }

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

        public PorudzbinaDodajViewModel()
        {
            IsIzmena = false;
            Options1 = new ObservableCollection<Option>();
            var ret = ListConverter<MusterijaModel>.KonvertujListu(MusterijaServis.Instance.CitajSve());
            foreach (var r in ret)
            {
                Option o = new Option();
                o.IsChecked = false;
                o.Display = r.JMBG;
                Options1.Add(o);
            }
            SelectedOption1 = null;
        }

        public PorudzbinaDodajViewModel(PorudzbinaModel modelUI)
        {
            this.modelUI = modelUI;
            Id = modelUI.Id;
            IsIzmena = true;

            Options1 = new ObservableCollection<Option>();
            var ret = ListConverter<PorudzbinaModel>.KonvertujListu(PorudzbinaServis.Instance.CitajSve());
            foreach (var r in ret)
            {
                Option o = new Option();
                o.IsChecked = false;
                o.Display = r.MusterijaId;
                Options1.Add(o);
            }
            SelectedOption1 = null;
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

        public bool CanDodaj(string id)
        {
            if (ValidacijaId(id))
                return true;
            return false;
        }

        public void Dodaj(string id)
        {
            

            if (!IsIzmena)
            {
                var option = PronadjiOznacen(Options1);
                if(option == null)
                {
                    ValidacionaGreska = "Morate oznaciti jednu opciju.";
                    return;
                }
                modelUI.MusterijaId = option.Display;
                modelUI.DatumPorudzbine = DateTime.Now;
                modelUI.Ukupno = "0";
                PorudzbinaServis.Instance.Dodaj(modelUI);
            }
            else
                PorudzbinaServis.Instance.Izmeni(modelUI);

            Manager.ShowWindow(new PorudzbinaViewModel());
            TryClose();
        }

        public void Nazad()
        {
            Manager.ShowWindow(new PorudzbinaViewModel());
            TryClose();
        }

        private Option PronadjiOznacen(ObservableCollection<Option> opcije)
        {
            foreach (var o in opcije)
            {
                if(o.IsChecked)
                    return o; 
            }

            return null;
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


        private void ValidacijaSvega()
        {
            if (!ValidacijaId(modelUI.Id))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Id");
            else
                ValidacionaGreska = "";
        }
        #endregion
    }
}
