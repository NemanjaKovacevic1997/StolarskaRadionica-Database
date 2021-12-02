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
    public class SegrtServis
    {
        private static SegrtServis _instance = null;

        public static SegrtServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SegrtServis();
                return _instance;
            }
        }

        private Repository<Segrt> _repozitorijum = new Repository<Segrt>();

        public void Dodaj(SegrtModel modelUI)
        {
            Segrt modelDB = new Segrt();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public SegrtModel Citaj(int id)
        {
            Segrt modelDB = _repozitorijum.Get(new object[] { id });

            if (modelDB != null)
            {
                SegrtModel modelUI = new SegrtModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<SegrtModel> CitajSve()
        {
            ICollection<SegrtModel> modelsUI = new List<SegrtModel>();

            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            SegrtModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new SegrtModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(SegrtModel modelUI)
        {
            Segrt modelDB = _repozitorijum.Get(new object[] { int.Parse(modelUI.Id) });
            IzUIModelUModelBezKljuca(modelDB, modelUI);
            _repozitorijum.Update(modelDB, new object[] { int.Parse(modelUI.Id) });
        }

        public void Obrisi(int id)
        {
            _repozitorijum.Remove(new object[] { id });
        }

        private void IzUIModelUModel(Segrt modelDB, SegrtModel modelUI)
        {
            modelDB.Id = int.Parse(modelUI.Id);
            modelDB.Ime = modelUI.Ime;
            modelDB.Prezime = modelUI.Prezime;
            modelDB.Zanimanje = "Segrt";

            if (!string.IsNullOrEmpty(modelUI.Ocena))
                modelDB.Ocena = int.Parse(modelUI.Ocena);
            else
                modelDB.Ocena = null;

        }

        private void IzUIModelUModelBezKljuca(Segrt modelDB, SegrtModel modelUI)
        {
            modelDB.Ime = modelUI.Ime;
            modelDB.Prezime = modelUI.Prezime;
            modelDB.Zanimanje = "Segrt";

            if (!string.IsNullOrEmpty(modelUI.Ocena))
                modelDB.Ocena = int.Parse(modelUI.Ocena);
            else
                modelDB.Ocena = null;

        }

        private void IzModelUUIModel(Segrt modelDB, SegrtModel modelUI)
        {
            modelUI.Id = modelDB.Id.ToString();
            modelUI.Ime = modelDB.Ime;
            modelUI.Prezime = modelDB.Prezime;
            modelUI.Zanimanje = modelDB.Zanimanje;

            if (modelDB.Ocena == null)
                modelUI.Ocena = "";
            else
                modelUI.Ocena = modelDB.Ocena.ToString();
        }
    }
}
