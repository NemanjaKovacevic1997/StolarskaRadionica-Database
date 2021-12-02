using BazaPodataka.Model;
using KorisnickiInterfejs.Models;
using PristupBaziPodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Servis
{
    public class StavkaPorudzbineServis
    {

        private static StavkaPorudzbineServis _instance = null;

        public static StavkaPorudzbineServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StavkaPorudzbineServis();
                return _instance;
            }
        }

        private Repository<StavkaPorudzbine> _repozitorijum = new Repository<StavkaPorudzbine>();

        public void Dodaj(StavkaPorudzbineModel modelUI)
        {
            StavkaPorudzbine modelDB = new StavkaPorudzbine();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public StavkaPorudzbineModel Citaj(string musterijaJMBG, int porudzbinaId, int rbStavke)
        {
            StavkaPorudzbine modelDB = _repozitorijum.Get(new object[] { musterijaJMBG, porudzbinaId , rbStavke });

            if (modelDB != null)
            {
                StavkaPorudzbineModel modelUI = new StavkaPorudzbineModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<StavkaPorudzbineModel> CitajSve()
        {
            ICollection<StavkaPorudzbineModel> modelsUI = new List<StavkaPorudzbineModel>();

            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            StavkaPorudzbineModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new StavkaPorudzbineModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(StavkaPorudzbineModel modelUI)
        {
            var kljuc = new object[] { int.Parse(modelUI.PorudzbinaId), modelUI.PorudzbinaMusterijaId, int.Parse(modelUI.RedniBrojStavke)};
            StavkaPorudzbine modelDB = _repozitorijum.Get(kljuc);

            IzUIModelUModelBezKljuca(modelDB, modelUI);
            _repozitorijum.Update(modelDB, kljuc);
        }

        public void Obrisi(int porudzbinaId, string musterijaJMBG, int rbStavke)
        {
            var kljuc = new object[] { porudzbinaId , musterijaJMBG, rbStavke };
            _repozitorijum.Remove(kljuc);
        }

        private void IzUIModelUModel(StavkaPorudzbine modelDB, StavkaPorudzbineModel modelUI)
        {
            modelDB.Kolicina = int.Parse(modelUI.Kolicina);
            modelDB.PorudzbinaId = int.Parse(modelUI.PorudzbinaId);
            modelDB.PorudzbinaMusterijaId = modelUI.PorudzbinaMusterijaId;
            modelDB.RedniBrojStavke = int.Parse(modelUI.RedniBrojStavke);
            modelDB.RedniBrojStavkeCenovnika = int.Parse(modelUI.RedniBrojStavkeCenovnika);
        }

        private void IzUIModelUModelBezKljuca(StavkaPorudzbine modelDB, StavkaPorudzbineModel modelUI)
        {
            modelDB.Kolicina = int.Parse(modelUI.Kolicina);
            modelDB.RedniBrojStavkeCenovnika = int.Parse(modelUI.RedniBrojStavkeCenovnika);
        }

        private void IzModelUUIModel(StavkaPorudzbine modelDB, StavkaPorudzbineModel modelUI)
        {
            modelUI.Kolicina = modelDB.Kolicina.ToString();
            modelUI.PorudzbinaId = modelDB.PorudzbinaId.ToString();
            modelUI.PorudzbinaMusterijaId = modelDB.PorudzbinaMusterijaId;
            modelUI.RedniBrojStavke = modelDB.RedniBrojStavke.ToString();
            modelUI.RedniBrojStavkeCenovnika = modelDB.RedniBrojStavkeCenovnika.ToString();
        }
    }
}
