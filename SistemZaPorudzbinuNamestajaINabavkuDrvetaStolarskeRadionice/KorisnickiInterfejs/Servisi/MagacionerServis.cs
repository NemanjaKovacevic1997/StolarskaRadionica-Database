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
    public class MagacionerServis
    {
        private static MagacionerServis _instance = null;

        public static MagacionerServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MagacionerServis();
                return _instance;
            }
        }

        private Repository<Magacioner> _repozitorijum = new Repository<Magacioner>();

        public void Dodaj(MagacionerModel modelUI)
        {
            Magacioner modelDB = new Magacioner();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public MagacionerModel Citaj(int id)
        {
            Magacioner modelDB = _repozitorijum.Get(new object[] { id });

            if (modelDB != null)
            {
                MagacionerModel modelUI = new MagacionerModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<MagacionerModel> CitajSve()
        {
            ICollection<MagacionerModel> modelsUI = new List<MagacionerModel>();

            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            MagacionerModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new MagacionerModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(MagacionerModel modelUI)
        {
            Magacioner modelDB = _repozitorijum.Get(new object[] { int.Parse(modelUI.Id) });
            IzUIModelUModelBezKljuca(modelDB, modelUI);
            _repozitorijum.Update(modelDB, new object[] { int.Parse(modelUI.Id) });
        }

        public void Obrisi(int id)
        {
            _repozitorijum.Remove(new object[] { id });
        }

        private void IzUIModelUModel(Magacioner modelDB, MagacionerModel modelUI)
        {
            modelDB.Id = int.Parse(modelUI.Id);
            modelDB.Ime = modelUI.Ime;
            modelDB.Prezime = modelUI.Prezime;
            modelDB.StolarskaRadionicaId = modelUI.StolarskaRadionicaId;
            modelDB.Zanimanje = "Magacioner";
        }
        private void IzUIModelUModelBezKljuca(Magacioner modelDB, MagacionerModel modelUI)
        {
            modelDB.Ime = modelUI.Ime;
            modelDB.Prezime = modelUI.Prezime;
            modelDB.StolarskaRadionicaId = modelUI.StolarskaRadionicaId;
            modelDB.Zanimanje = "Magacioner";
        }

        private void IzModelUUIModel(Magacioner modelDB, MagacionerModel modelUI)
        {
            modelUI.Id = modelDB.Id.ToString();
            modelUI.Ime = modelDB.Ime;
            modelUI.Prezime = modelDB.Prezime;
            modelUI.StolarskaRadionicaId = modelDB.StolarskaRadionicaId;
            modelUI.Zanimanje = modelDB.Zanimanje;
        }
    }
}
