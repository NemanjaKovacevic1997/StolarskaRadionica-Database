using BazaPodataka.Model;
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
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.ViewModels
{
    public class ZahtevDodajViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private ZahtevModel modelUI = new ZahtevModel();
        private FabrikaPoruka _fabrikaPoruka = new FabrikaPoruka();
        private Repository<Sadrzi> repo = new Repository<Sadrzi>();
        private IEnumerable<Sadrzi> sadrzii;

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

        private ObservableCollection<OptionFull> _options2;
        public ObservableCollection<OptionFull> Options2
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

        public ZahtevDodajViewModel()
        {
            IsIzmena = false;
            Options1 = new ObservableCollection<Option>();
            var ret = ListConverter<MagacionerModel>.KonvertujListu(MagacionerServis.Instance.CitajSve());
            foreach (var r in ret)
            {
                Option o = new Option();
                o.IsChecked = false;
                o.Display = r.Id;
                Options1.Add(o);
            }

            Options2 = new ObservableCollection<OptionFull>();
            var ret2 = ListConverter<VrstaDrvetaModel>.KonvertujListu(VrstaDrvetaServis.Instance.CitajSve());
            foreach (var r in ret2)
            {
                OptionFull o = new OptionFull(r.Id);
                Options2.Add(o);
            }
        }

        public ZahtevDodajViewModel(ZahtevModel modelUI)
        {
            this.modelUI = modelUI;
            Id = modelUI.Id;
            IsIzmena = true;

            Options1 = new ObservableCollection<Option>();
            var ret = ListConverter<MagacionerModel>.KonvertujListu(MagacionerServis.Instance.CitajSve());
            foreach (var r in ret)
            {
                Option o = new Option();
                o.Display = r.Id;
                if (o.Display == modelUI.MagacionerId.ToString())
                    o.IsChecked = true;
                else
                    o.IsChecked = false;
                
                Options1.Add(o);
            }

            Options2 = new ObservableCollection<OptionFull>();
            var ret2 = ListConverter<VrstaDrvetaModel>.KonvertujListu(VrstaDrvetaServis.Instance.CitajSve());
            
            Repository<Sadrzi> repository = new Repository<Sadrzi>();
            sadrzii = repository.GetAll().Where(i => i.ZahtevId.ToString() == modelUI.Id);
            
            foreach (var vd in ret2)
            {
                OptionFull o = new OptionFull(vd.Id);
                foreach (var sadrzi in sadrzii)
                {
                    if(sadrzi.VrstaDrvetaId.ToString() == vd.Id && sadrzi.ZahtevId.ToString() == modelUI.Id)
                    {
                        o.Kolicina = sadrzi.Kolicina.ToString();
                        o.IsChecked = true;
                    }
                }

                Options2.Add(o);
            }
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
                if (option == null)
                {
                    ValidacionaGreska = "Morate oznaciti jednu opciju za magacionera.";
                    return;
                }

                var options = PronadjiOznacene(Options2);
                if (options.Count == 0)
                {
                    ValidacionaGreska = "Morate dodati makar jednu vrstu drveta.";
                    return;
                }

                modelUI.MagacionerId = int.Parse(option.Display);
                modelUI.DatumNastanka = DateTime.Now;
                ZahtevServis.Instance.Dodaj(modelUI);

                foreach (var o in options)
                {
                    if (!int.TryParse(o.Kolicina, out int kolicina))
                        continue;
                    repo.Add(new Sadrzi()
                    {
                        ZahtevId = int.Parse(modelUI.Id),
                        VrstaDrvetaId = int.Parse(o.Display),
                        Kolicina = kolicina
                    });
                }
            }
            else
            {
                var options = PronadjiOznacene(Options2);
                if (options.Count == 0)
                {
                    ValidacionaGreska = "Morate dodati makar jednu vrstu drveta.";
                    return;
                }

                foreach (var s in sadrzii)
                    repo.Remove(new object[] { s.VrstaDrvetaId, s.ZahtevId });

                foreach (var o in options)
                {
                    if (!int.TryParse(o.Kolicina, out int kolicina))
                        continue;
                    repo.AddOrUpdate(new Sadrzi()
                    {
                        ZahtevId = int.Parse(modelUI.Id),
                        VrstaDrvetaId = int.Parse(o.Display),
                        Kolicina = kolicina
                    });
                }
            }
                

            Manager.ShowWindow(new ZahtevViewModel());
            TryClose();
        }

        public void Nazad()
        {
            Manager.ShowWindow(new ZahtevViewModel());
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

        private List<OptionFull> PronadjiOznacene(ObservableCollection<OptionFull> opcije)
        {
            List<OptionFull> ret = new List<OptionFull>();
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
            if (string.IsNullOrEmpty(str) || !int.TryParse(str, out int br))
                return false;

            if (br <= 0)
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