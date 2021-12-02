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
    class MagacinServis
    {
        private static MagacinServis _instance = null;

        public static MagacinServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MagacinServis();
                return _instance;
            }
        }

        private Repository<Magacin> _repozitorijum = new Repository<Magacin>();

        public void Dodaj(MagacinModel modelUI)
        {
            Magacin modelDB = new Magacin();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public MagacinModel Citaj(int id)
        {
            Magacin modelDB = _repozitorijum.Get(new object[] { id });

            if (modelDB != null)
            {
                MagacinModel modelUI = new MagacinModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<MagacinModel> CitajSve()
        {
            ICollection<MagacinModel> modelsUI = new List<MagacinModel>();

            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            MagacinModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new MagacinModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(MagacinModel modelUI)
        {
            var kljuc = new object[] { int.Parse(modelUI.Id) };
            Magacin modelDB = _repozitorijum.Get(kljuc);
            _repozitorijum.Update(modelDB, kljuc);
        }

        public void Obrisi(int id)
        {
            _repozitorijum.Remove(new object[] { id });
        }



        private void IzUIModelUModel(Magacin modelDB, MagacinModel modelUI)
        {
            modelDB.Id = int.Parse(modelUI.Id);
        }

        private void IzModelUUIModel(Magacin modelDB, MagacinModel modelUI)
        {
            modelUI.Id = modelDB.Id.ToString();
        }
    }
}
