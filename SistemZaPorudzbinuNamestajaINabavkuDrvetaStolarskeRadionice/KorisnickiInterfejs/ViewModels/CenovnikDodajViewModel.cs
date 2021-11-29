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
    public class CenovnikDodajViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private CenovnikModel modelUI = new CenovnikModel();
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

        public CenovnikDodajViewModel()
        {
            IsIzmena = false;
            Inicijalizacija();
        }

        public CenovnikDodajViewModel(CenovnikModel modelUI)
        {
            this.modelUI = modelUI;
            IsIzmena = true;
            Inicijalizacija(modelUI.StolarskaRadionicaId, modelUI.StolarId);
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

        public void Dodaj()
        {
            if (!IsIzmena)
            {
                var option1 = PronadjiOznacen(Options1);
                if (option1 == null)
                {
                    ValidacionaGreska = "Morate oznaciti jednu opciju za stolarsku radionicu.";
                    return;
                }

                modelUI.StolarskaRadionicaId = option1.Display;

                var option2 = PronadjiOznacen(Options2);
                if (option2 == null)
                    modelUI.StolarId = "";
                else
                    modelUI.StolarId = option2.Display;

                CenovnikServis.Instance.Dodaj(modelUI);
            }
            else
            {
                var option2 = PronadjiOznacen(Options2);
                if (option2 == null)
                    modelUI.StolarId = "";
                else
                    modelUI.StolarId = option2.Display;

                CenovnikServis.Instance.Izmeni(modelUI);
            }
                

            Manager.ShowWindow(new CenovnikViewModel());
            TryClose();
        }

        public void Nazad()
        {
            Manager.ShowWindow(new CenovnikViewModel());
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
            var ret1 = ListConverter<StolarskaRadionicaModel>.KonvertujListu(StolarskaRadionicaServis.Instance.CitajSve());
            foreach (var r1 in ret1)
            {
                Option o = new Option();
                o.Display = r1.Id;
                if (oznacen1 != "" && o.Display == oznacen1)
                    o.IsChecked = true;
                else
                    o.IsChecked = false;

                Options1.Add(o);
            }

            var ret2 = ListConverter<StolarModel>.KonvertujListu(StolarServis.Instance.CitajSve());
            Options2.Add(new Option() { Display = "", IsChecked = (oznacen2 == "") ? true : false });

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

        #endregion
    }
}
