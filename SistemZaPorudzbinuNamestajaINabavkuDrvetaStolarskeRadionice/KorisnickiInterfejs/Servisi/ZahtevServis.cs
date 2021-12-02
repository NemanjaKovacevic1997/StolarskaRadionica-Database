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
    public class ZahtevServis
    {
        private static ZahtevServis _instance = null;

        public static ZahtevServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ZahtevServis();
                return _instance;
            }
        }

        private Repository<Zahtev> _repozitorijum = new Repository<Zahtev>();

        public void Dodaj(ZahtevModel modelUI)
        {
            Zahtev modelDB = new Zahtev();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public ZahtevModel Citaj(int id)
        {
            Zahtev modelDB = _repozitorijum.Get(new object[] { id });

            if (modelDB != null)
            {
                ZahtevModel modelUI = new ZahtevModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<ZahtevModel> CitajSve()
        {
            ICollection<ZahtevModel> modelsUI = new List<ZahtevModel>();

            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            ZahtevModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new ZahtevModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(ZahtevModel modelUI)
        {
            var kljuc = new object[] { int.Parse(modelUI.Id) };
            Zahtev modelDB = _repozitorijum.Get(kljuc);
            IzUIModelUModelBezKljuca(modelDB, modelUI);
            _repozitorijum.Update(modelDB, kljuc);
        }

        public void Obrisi(int id)
        {
            _repozitorijum.Remove(new object[] { id });
        }



        private void IzUIModelUModel(Zahtev modelDB, ZahtevModel modelUI)
        {
            modelDB.Id = int.Parse(modelUI.Id);
            modelDB.DatumNastanka = modelUI.DatumNastanka;
            modelDB.MagacionerId = modelUI.MagacionerId;
        }
        private void IzUIModelUModelBezKljuca(Zahtev modelDB, ZahtevModel modelUI)
        {
            modelDB.DatumNastanka = modelUI.DatumNastanka;
            modelDB.MagacionerId = modelUI.MagacionerId;
        }

        private void IzModelUUIModel(Zahtev modelDB, ZahtevModel modelUI)
        {
            modelUI.Id = modelDB.Id.ToString();
            modelUI.DatumNastanka = modelDB.DatumNastanka;
            modelUI.MagacionerId = modelDB.MagacionerId;
        }
    }
}