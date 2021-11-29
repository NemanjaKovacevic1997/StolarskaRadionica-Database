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
    public class StolarServis
    {
        private static StolarServis _instance = null;

        public static StolarServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StolarServis();
                return _instance;
            }
        }

        private Repository<Stolar> _repozitorijum = new Repository<Stolar>();

        public void Dodaj(StolarModel modelUI)
        {
            Stolar modelDB = new Stolar();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public StolarModel Citaj(int id)
        {
            Stolar modelDB = _repozitorijum.Get(new object[] { id });

            if (modelDB != null)
            {
                StolarModel modelUI = new StolarModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<StolarModel> CitajSve()
        {
            ICollection<StolarModel> modelsUI = new List<StolarModel>();

            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            StolarModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new StolarModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(StolarModel modelUI)
        {
            Stolar modelDB = _repozitorijum.Get(new object[] { int.Parse(modelUI.Id) }); //new Stolar();
            IzUIModelUModelBezKljuca(modelDB, modelUI);
            //_repozitorijum.SaveChanges();
            _repozitorijum.Update(modelDB, new object[] { int.Parse(modelUI.Id) });
        }

        public void Obrisi(int id)
        {
            _repozitorijum.Remove(new object[] { id });
        }

        private void IzUIModelUModel(Stolar modelDB, StolarModel modelUI)
        {
            modelDB.Id = int.Parse(modelUI.Id);
            modelDB.Ime = modelUI.Ime;
            modelDB.Prezime = modelUI.Prezime;
            modelDB.StolarskaRadionicaId = modelUI.StolarskaRadionicaId;
            modelDB.Zanimanje = "Stolar";
        }

        private void IzUIModelUModelBezKljuca(Stolar modelDB, StolarModel modelUI)
        { 
            modelDB.Ime = modelUI.Ime;
            modelDB.Prezime = modelUI.Prezime;
            modelDB.StolarskaRadionicaId = modelUI.StolarskaRadionicaId;
            modelDB.Zanimanje = "Stolar";
        }

        private void IzModelUUIModel(Stolar modelDB, StolarModel modelUI)
        {
            modelUI.Id = modelDB.Id.ToString();
            modelUI.Ime = modelDB.Ime;
            modelUI.Prezime = modelDB.Prezime;
            modelUI.StolarskaRadionicaId = modelDB.StolarskaRadionicaId;
            modelUI.Zanimanje = modelDB.Zanimanje;
        }
    }
}
