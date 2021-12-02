using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BazaPodataka.Model.Vise_ViseEntiteti;
using Caliburn.Micro;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using KorisnickiInterfejs.Views;
using PristupBaziPodataka;

namespace KorisnickiInterfejs.ViewModels
{
    public class MagacinDodajViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private FabrikaPoruka _fabrikaPoruka = new FabrikaPoruka();
        private MagacinModel modelUI = new MagacinModel();
        private Repository<Skladisti> repo = new Repository<Skladisti>();
        private IEnumerable<Skladisti> sadrzii;

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

        public MagacinDodajViewModel()
        {
            IsIzmena = false;
            Inicijalizacija1();
        }

        public MagacinDodajViewModel(MagacinModel modelUI)
        {
            this.modelUI = modelUI;
            IsIzmena = true;

            Inicijalizacija(modelUI.Id);

            Options2 = new ObservableCollection<OptionFull>();
            var ret2 = ListConverter<VrstaDrvetaModel>.KonvertujListu(VrstaDrvetaServis.Instance.CitajSve());

            Repository<Skladisti> repository = new Repository<Skladisti>();
            sadrzii = repository.GetAll().Where(i => i.MagacinId.ToString() == modelUI.Id);

            foreach (var vd in ret2)
            {
                OptionFull o = new OptionFull(vd.Id);
                foreach (var sadrzi in sadrzii)
                {
                    if (sadrzi.VrstaDrvetaId.ToString() == vd.Id && sadrzi.MagacinId.ToString() == modelUI.Id)
                    {
                        o.Kolicina = sadrzi.Kolicina.ToString();
                        o.IsChecked = true;
                    }
                }

                Options2.Add(o);
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

        public void Dodaj()
        {
            if (!IsIzmena)
            {
                MagacinServis.Instance.Dodaj(modelUI);

                var options = PronadjiOznacene(Options2);
                foreach (var o in options)
                {
                    if (!int.TryParse(o.Kolicina, out int kolicina))
                        continue;
                    repo.AddOrUpdate(new Skladisti()
                    {
                        MagacinId = int.Parse(modelUI.Id),
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
                    repo.Remove(new object[] { s.VrstaDrvetaId, s.MagacinId });

                foreach (var o in options)
                {
                    if (!int.TryParse(o.Kolicina, out int kolicina))
                        continue;
                    repo.AddOrUpdate(new Skladisti()
                    {
                        MagacinId = int.Parse(modelUI.Id),
                        VrstaDrvetaId = int.Parse(o.Display),
                        Kolicina = kolicina
                    });
                }
            }

            Manager.ShowWindow(new MagacinViewModel());
            TryClose();
        }

        public void Nazad()
        {
            Manager.ShowWindow(new MagacinViewModel());
            TryClose();
        }


        private void Inicijalizacija1(string oznacen1 = "")
        {
            Inicijalizacija(oznacen1);
            Options2 = new ObservableCollection<OptionFull>();
            var ret2 = ListConverter<VrstaDrvetaModel>.KonvertujListu(VrstaDrvetaServis.Instance.CitajSve());
            foreach (var r in ret2)
            {
                OptionFull o = new OptionFull(r.Id);
                Options2.Add(o);
            }
        }

        private void Inicijalizacija(string id)
        {
            modelUI.Id = id;
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