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
    public class VrstaDrvetaServis
    {
        private static VrstaDrvetaServis _instance = null;

        public static VrstaDrvetaServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VrstaDrvetaServis();
                return _instance;
            }
        }

        private Repository<VrstaDrveta> _repozitorijum = new Repository<VrstaDrveta>();

        public void Dodaj(VrstaDrvetaModel modelUI)
        {
            VrstaDrveta modelDB = new VrstaDrveta();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public VrstaDrvetaModel Citaj(int id)
        {
            VrstaDrveta modelDB = _repozitorijum.Get(new object[] { id });

            if (modelDB != null)
            {
                VrstaDrvetaModel modelUI = new VrstaDrvetaModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<VrstaDrvetaModel> CitajSve()
        {
            ICollection<VrstaDrvetaModel> modelsUI = new List<VrstaDrvetaModel>();

            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            VrstaDrvetaModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new VrstaDrvetaModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(VrstaDrvetaModel modelUI)
        {
            var kljuc = new object[] { int.Parse(modelUI.Id) };
            VrstaDrveta modelDB = _repozitorijum.Get(kljuc);
            IzUIModelUModelBezKljuca(modelDB, modelUI);
            _repozitorijum.Update(modelDB, kljuc);
        }

        public void Obrisi(int id)
        {
            _repozitorijum.Remove(new object[] { id });
        }

        private void IzUIModelUModel(VrstaDrveta modelDB, VrstaDrvetaModel modelUI)
        {
            modelDB.Id = int.Parse(modelUI.Id);
            modelDB.Naziv = modelUI.Naziv;
        }

        private void IzUIModelUModelBezKljuca(VrstaDrveta modelDB, VrstaDrvetaModel modelUI)
        {
            modelDB.Naziv = modelUI.Naziv;
        }

        private void IzModelUUIModel(VrstaDrveta modelDB, VrstaDrvetaModel modelUI)
        {
            modelUI.Id = modelDB.Id.ToString();
            modelUI.Naziv = modelDB.Naziv;
        }
    }
}