using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BazaPodataka;
using BazaPodataka.Model;
using KorisnickiInterfejs.Models;
using PristupBaziPodataka;

namespace KorisnickiInterfejs.Servisi
{
    public class VrstaNamestajaServis
    {
        private static VrstaNamestajaServis _instance = null;

        public static VrstaNamestajaServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VrstaNamestajaServis();
                return _instance;
            }
        }

        private Repository<VrstaNamestaja> _repozitorijum = new Repository<VrstaNamestaja>();

        public void Dodaj(VrstaNamestajaModel modelUI)
        {
            VrstaNamestaja modelDB = new VrstaNamestaja();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public VrstaNamestajaModel Citaj(int id)
        {
            VrstaNamestaja modelDB = _repozitorijum.Get(new object[] { id });

            if (modelDB != null)
            {
                VrstaNamestajaModel modelUI = new VrstaNamestajaModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<VrstaNamestajaModel> CitajSve()
        {
            ICollection<VrstaNamestajaModel> modelsUI = new List<VrstaNamestajaModel>();

            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            VrstaNamestajaModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new VrstaNamestajaModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(VrstaNamestajaModel modelUI)
        {
            var kljuc = new object[] { int.Parse(modelUI.Id) };
            VrstaNamestaja modelDB = _repozitorijum.Get(kljuc);
            IzUIModelUModelBezKljuca(modelDB, modelUI);
            _repozitorijum.Update(modelDB, kljuc);
        }

        public void Obrisi(int id)
        {
            _repozitorijum.Remove(new object[] { id });
        }


        private void IzUIModelUModelBezKljuca(VrstaNamestaja modelDB, VrstaNamestajaModel modelUI)
        {
            modelDB.Materijal = modelUI.Materijal;
            modelDB.Naziv = modelUI.Naziv;
        }

        private void IzUIModelUModel(VrstaNamestaja modelDB, VrstaNamestajaModel modelUI)
        {
            modelDB.Id = int.Parse(modelUI.Id);
            modelDB.Materijal = modelUI.Materijal;
            modelDB.Naziv = modelUI.Naziv;
        }

        private void IzModelUUIModel(VrstaNamestaja modelDB, VrstaNamestajaModel modelUI)
        {
            modelUI.Id = modelDB.Id.ToString();
            modelUI.Materijal = modelDB.Materijal;
            modelUI.Naziv = modelDB.Naziv;
        }
    }
}
