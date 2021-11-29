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
    public class MusterijaServis
    {
        private static MusterijaServis _instance = null;

        public static MusterijaServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MusterijaServis();
                return _instance;
            }
        }

        private Repository<Musterija> _repozitorijum = new Repository<Musterija>();

        public void Dodaj(MusterijaModel modelUI)
        {
            Musterija modelDB = new Musterija();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public MusterijaModel Citaj(string id)
        {
            Musterija modelDB = _repozitorijum.Get(new object[] { id });

            if (modelDB != null)
            {
                MusterijaModel modelUI = new MusterijaModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<MusterijaModel> CitajSve()
        {
            ICollection<MusterijaModel> modelsUI = new List<MusterijaModel>();
            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            MusterijaModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new MusterijaModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(MusterijaModel modelUI)
        {
            Musterija modelDB = _repozitorijum.Get(new object[] { modelUI.JMBG });
            IzUIModelUModelBezKljuca(modelDB, modelUI);
            _repozitorijum.Update(modelDB, new object[] { modelUI.JMBG });
        }

        public void Obrisi(string id)
        {
            _repozitorijum.Remove(new object[] { id });
        }

        private void IzUIModelUModel(Musterija modelDB, MusterijaModel modelUI)
        {
            modelDB.JMBG = modelUI.JMBG;
            modelDB.Ime = modelUI.Ime;
            modelDB.Prezime = modelUI.Prezime;
        }
        private void IzUIModelUModelBezKljuca(Musterija modelDB, MusterijaModel modelUI)
        {
            modelDB.Ime = modelUI.Ime;
            modelDB.Prezime = modelUI.Prezime;
        }


        private void IzModelUUIModel(Musterija modelDB, MusterijaModel modelUI)
        {
            modelUI.JMBG = modelDB.JMBG;
            modelUI.Ime = modelDB.Ime;
            modelUI.Prezime = modelDB.Prezime;
        }
    }
}
