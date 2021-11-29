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
    public class CenovnikServis
    {
        private static CenovnikServis _instance = null;

        public static CenovnikServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CenovnikServis();
                return _instance;
            }
        }

        private Repository<Cenovnik> _repozitorijum = new Repository<Cenovnik>();

        public void Dodaj(CenovnikModel modelUI)
        {
            Cenovnik modelDB = new Cenovnik();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public CenovnikModel Citaj(int id)
        {
            Cenovnik modelDB = _repozitorijum.Get(new object[] { id });

            if (modelDB != null)
            {
                CenovnikModel modelUI = new CenovnikModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<CenovnikModel> CitajSve()
        {
            ICollection<CenovnikModel> modelsUI = new List<CenovnikModel>();

            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            CenovnikModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new CenovnikModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(CenovnikModel modelUI)
        {
            var kljuc = new object[] { int.Parse(modelUI.StolarskaRadionicaId) };
            Cenovnik modelDB = _repozitorijum.Get(kljuc);

            IzUIModelUModelBezKljuca(modelDB, modelUI);
            _repozitorijum.Update(modelDB, kljuc);
        }

        public void Obrisi(int id)
        {
            _repozitorijum.Remove(new object[] { id });
        }

        private void IzUIModelUModelBezKljuca(Cenovnik modelDB, CenovnikModel modelUI)
        {
            if (modelUI.StolarId == "")
                modelDB.StolarId = null;
            else
                modelDB.StolarId = int.Parse(modelUI.StolarId);
        }

        private void IzUIModelUModel(Cenovnik modelDB, CenovnikModel modelUI)
        {
            if (modelUI.StolarId == "")
                modelDB.StolarId = null;
            else
                modelDB.StolarId = int.Parse(modelUI.StolarId);
            modelDB.StolarskaRadionicaId = int.Parse(modelUI.StolarskaRadionicaId);
        }

        private void IzModelUUIModel(Cenovnik modelDB, CenovnikModel modelUI)
        {
            if (modelDB.StolarId == null)
                modelUI.StolarId = "";
            else
                modelUI.StolarId = modelDB.StolarId.ToString();
            modelUI.StolarskaRadionicaId = modelDB.StolarskaRadionicaId.ToString();
        }
    }
}
