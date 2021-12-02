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
    public class PorudzbinaServis
    {
        private static PorudzbinaServis _instance = null;

        public static PorudzbinaServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PorudzbinaServis();
                return _instance;
            }
        }

        private Repository<Porudzbina> _repozitorijum = new Repository<Porudzbina>();

        public void Dodaj(PorudzbinaModel modelUI)
        {
            Porudzbina modelDB = new Porudzbina();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public PorudzbinaModel Citaj(string musterijaJMBG, int porudzbinaId)
        {
            Porudzbina modelDB = new Porudzbina();

            modelDB = _repozitorijum.Get(new object[] { musterijaJMBG, porudzbinaId });

            if (modelDB != null)
            {
                PorudzbinaModel modelUI = new PorudzbinaModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<PorudzbinaModel> CitajSve()
        {
            ICollection<PorudzbinaModel> modelsUI = new List<PorudzbinaModel>();

            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            PorudzbinaModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new PorudzbinaModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(PorudzbinaModel modelUI)
        {
            var kljuc = new object[] { modelUI.MusterijaId, modelUI.Id };
            Porudzbina modelDB = _repozitorijum.Get(kljuc);

            IzUIModelUModelBezKljuca(modelDB, modelUI);
            _repozitorijum.Update(modelDB, kljuc);
        }

        public void Obrisi(string musterijaJMBG, int porudzbinaId)
        {
            var kljuc = new object[] { musterijaJMBG, porudzbinaId };
            _repozitorijum.Remove(kljuc);
        }



        private void IzUIModelUModel(Porudzbina modelDB, PorudzbinaModel modelUI)
        {
            modelDB.Id = int.Parse(modelUI.Id);
            modelDB.MusterijaId = modelUI.MusterijaId;
            modelDB.Ukupno = double.Parse(modelUI.Ukupno);
            modelDB.DatumPorudzbine = modelUI.DatumPorudzbine;
        }
        private void IzUIModelUModelBezKljuca(Porudzbina modelDB, PorudzbinaModel modelUI)
        {
            modelDB.Ukupno = double.Parse(modelUI.Ukupno);
            modelDB.DatumPorudzbine = modelUI.DatumPorudzbine;
        }

        private void IzModelUUIModel(Porudzbina modelDB, PorudzbinaModel modelUI)
        {
            modelUI.Id = modelDB.Id.ToString();
            modelUI.MusterijaId = modelDB.MusterijaId;
            modelUI.Ukupno = modelDB.Ukupno.ToString();
            modelUI.DatumPorudzbine = modelDB.DatumPorudzbine;
        }
    }
}
