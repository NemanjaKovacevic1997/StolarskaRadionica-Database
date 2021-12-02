using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BazaPodataka.Model;
using BazaPodataka.Model.Vise_ViseEntiteti;
using Caliburn.Micro;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using KorisnickiInterfejs.Views;
using PristupBaziPodataka;

namespace KorisnickiInterfejs.ViewModels
{
    class DobavljacDrvetaDodajViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private DobavljacDrvetaModel modelUI = new DobavljacDrvetaModel();
        private FabrikaPoruka _fabrikaPoruka = new FabrikaPoruka();
        private Repository<Nudi> repo = new Repository<Nudi>();
        private IEnumerable<Nudi> nudii;

        private ObservableCollection<OptionFullFull> _options2;
        public ObservableCollection<OptionFullFull> Options2
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

        public DobavljacDrvetaDodajViewModel()
        {
            IsIzmena = false;
         
            Options2 = new ObservableCollection<OptionFullFull>();
            var ret2 = ListConverter<VrstaDrvetaModel>.KonvertujListu(VrstaDrvetaServis.Instance.CitajSve());
            foreach (var r in ret2)
            {
                OptionFullFull o = new OptionFullFull(r.Id);
                Options2.Add(o);
            }
        }

        public DobavljacDrvetaDodajViewModel(DobavljacDrvetaModel modelUI)
        {
            this.modelUI = modelUI;
            Id = modelUI.Id.ToString();
            Naziv = modelUI.Naziv;
            Mesto = modelUI.Mesto;
            Ulica = modelUI.Ulica;
            Broj = modelUI.Broj;
            IsIzmena = true;

            Options2 = new ObservableCollection<OptionFullFull>();
            var ret2 = ListConverter<VrstaDrvetaModel>.KonvertujListu(VrstaDrvetaServis.Instance.CitajSve());

            Repository<Nudi> repository = new Repository<Nudi>();
            nudii = repository.GetAll().Where(i => i.DobavljacDrvetaId.ToString() == modelUI.Id);

            foreach (var vd in ret2)
            {
                OptionFullFull o = new OptionFullFull(vd.Id);
                foreach (var nudi in nudii)
                {
                    if (nudi.VrstaDrvetaId.ToString() == vd.Id && nudi.DobavljacDrvetaId.ToString() == modelUI.Id)
                    {
                        o.Kolicina = nudi.Kolicina.ToString();
                        o.Cena = nudi.CenaPoKubnomMetru.ToString();
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

        public string Mesto
        {
            get { return modelUI.Mesto; }
            set
            {
                modelUI.Mesto = value;
                if (!ValidacijaMesto(modelUI.Mesto))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Mesto");
                else
                    ValidacijaSvega();
            }
        }

        public string Ulica
        {
            get { return modelUI.Ulica; }
            set
            {
                modelUI.Ulica = value;
                if (!ValidacijaUlica(modelUI.Ulica))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Ulica");
                else
                    ValidacijaSvega();
            }
        }

        public string Broj
        {
            get { return modelUI.Broj; }
            set
            {
                modelUI.Broj = value;
                if (!ValidacijaBroj(modelUI.Broj))
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
     
                var options = PronadjiOznacene(Options2);
             
                DobavljacDrvetaServis.Instance.Dodaj(modelUI);

                foreach (var o in options)
                {
                    if (!int.TryParse(o.Kolicina, out int kolicina))
                        continue;
                    if (!int.TryParse(o.Cena, out int cena))
                        continue;
                    repo.Add(new Nudi()
                    {
                        DobavljacDrvetaId = int.Parse(modelUI.Id),
                        VrstaDrvetaId = int.Parse(o.Display),
                        Kolicina = kolicina,
                        CenaPoKubnomMetru = cena
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

                DobavljacDrvetaServis.Instance.Izmeni(modelUI);

                foreach (var nudi in nudii)
                    repo.Remove(new object[] { nudi.VrstaDrvetaId, nudi.DobavljacDrvetaId });

                foreach (var o in options)
                {
                    if (!int.TryParse(o.Kolicina, out int kolicina))
                        continue;
                    if (!int.TryParse(o.Cena, out int cena))
                        continue;

                    repo.AddOrUpdate(new Nudi()
                    {
                        DobavljacDrvetaId = int.Parse(modelUI.Id),
                        VrstaDrvetaId = int.Parse(o.Display),
                        Kolicina = kolicina,
                        CenaPoKubnomMetru = cena
                    });
                }
            }
                

            Manager.ShowWindow(new DobavljacDrvetaViewModel());
            TryClose();
        }

        public void Nazad()
        {
            Manager.ShowWindow(new DobavljacDrvetaViewModel());
            TryClose();
        }

        private List<OptionFullFull> PronadjiOznacene(ObservableCollection<OptionFullFull> opcije)
        {
            List<OptionFullFull> ret = new List<OptionFullFull>();
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
            if (!ValidacijaId(modelUI.Id))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Id");
            else if (!ValidacijaNaziv(modelUI.Naziv))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Naziv");
            else if (!ValidacijaMesto(modelUI.Mesto))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Mesto");
            else if (!ValidacijaUlica(modelUI.Ulica))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Ulica");
            else if (!ValidacijaBroj(modelUI.Broj))
                ValidacionaGreska = _fabrikaPoruka.Uzmi("Broj");
            else
                ValidacionaGreska = "";
        }
        #endregion
    }
}
