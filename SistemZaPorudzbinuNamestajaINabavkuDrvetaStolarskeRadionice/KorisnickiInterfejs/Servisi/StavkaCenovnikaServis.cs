using BazaPodataka.Model;
using KorisnickiInterfejs.Models;
using PristupBaziPodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Servisi
{
    public class StavkaCenovnikaServis
    {
        private static StavkaCenovnikaServis _instance = null;

        public static StavkaCenovnikaServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StavkaCenovnikaServis();
                return _instance;
            }
        }

        private Repository<StavkaCenovnika> _repozitorijum = new Repository<StavkaCenovnika>();

        public void Dodaj(StavkaCenovnikaModel modelUI)
        {
            StavkaCenovnika modelDB = new StavkaCenovnika();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public StavkaCenovnikaModel Citaj(int cenovnikId, int redniBrojStavke)
        {
            StavkaCenovnika modelDB = new StavkaCenovnika();
            modelDB = _repozitorijum.Get(new object[] { cenovnikId, redniBrojStavke });

            if (modelDB != null)
            {
                StavkaCenovnikaModel modelUI = new StavkaCenovnikaModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<StavkaCenovnikaModel> CitajSve()
        {
            ICollection<StavkaCenovnikaModel> modelsUI = new List<StavkaCenovnikaModel>();

            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            StavkaCenovnikaModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new StavkaCenovnikaModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(StavkaCenovnikaModel modelUI)
        {
            var kljuc = new object[] { int.Parse(modelUI.CenovnikId), int.Parse(modelUI.RedniBrojStavke) };
            StavkaCenovnika modelDB = _repozitorijum.Get(kljuc);

            IzUIModelUModelBezKljuca(modelDB, modelUI);
            _repozitorijum.Update(modelDB, kljuc);
        }

        public void Obrisi(int cenovnikId, int redniBrojStavke)
        {
            var kljuc = new object[] { cenovnikId, redniBrojStavke };
            _repozitorijum.Remove(kljuc);
        }

        private void IzUIModelUModel(StavkaCenovnika modelDB, StavkaCenovnikaModel modelUI)
        {
            modelDB.CenovnikId = int.Parse(modelUI.CenovnikId);
            modelDB.RedniBrojStavke = int.Parse(modelUI.RedniBrojStavke);
            modelDB.Cena = double.Parse(modelUI.Cena);
            modelDB.VrstaNamestajaId = int.Parse(modelUI.VrstaNamestajaId);
        }

        private void IzUIModelUModelBezKljuca(StavkaCenovnika modelDB, StavkaCenovnikaModel modelUI)
        {
            modelDB.Cena = double.Parse(modelUI.Cena);
            modelDB.VrstaNamestajaId = int.Parse(modelUI.VrstaNamestajaId);
        }

        private void IzModelUUIModel(StavkaCenovnika modelDB, StavkaCenovnikaModel modelUI)
        {
            modelUI.CenovnikId = modelDB.CenovnikId.ToString();
            modelUI.RedniBrojStavke = modelDB.RedniBrojStavke.ToString();
            modelUI.Cena = modelDB.Cena.ToString();
            modelUI.VrstaNamestajaId = modelDB.VrstaNamestajaId.ToString();
        }
    }
}
