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
    class DobavljacDrvetaServis
    {
        private static DobavljacDrvetaServis _instance = null;

        public static DobavljacDrvetaServis Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DobavljacDrvetaServis();
                return _instance;
            }
        }

        private Repository<DobavljacDrveta> _repozitorijum = new Repository<DobavljacDrveta>();

        public void Dodaj(DobavljacDrvetaModel modelUI)
        {
            DobavljacDrveta modelDB = new DobavljacDrveta();
            IzUIModelUModel(modelDB, modelUI);
            _repozitorijum.Add(modelDB);
        }

        public DobavljacDrvetaModel Citaj(int id)
        {
            DobavljacDrveta modelDB = _repozitorijum.Get(new object[] { id });

            if (modelDB != null)
            {
                DobavljacDrvetaModel modelUI = new DobavljacDrvetaModel();
                IzModelUUIModel(modelDB, modelUI);
                return modelUI;
            }

            return null;
        }

        public ICollection<DobavljacDrvetaModel> CitajSve()
        {
            ICollection<DobavljacDrvetaModel> modelsUI = new List<DobavljacDrvetaModel>();

            var modelsDB = _repozitorijum.GetAll();

            if (modelsDB == null)
                return null;

            DobavljacDrvetaModel modelUI;
            foreach (var modelDB in modelsDB)
            {
                modelUI = new DobavljacDrvetaModel();
                IzModelUUIModel(modelDB, modelUI);
                modelsUI.Add(modelUI);
            }

            return modelsUI;
        }

        public void Izmeni(DobavljacDrvetaModel modelUI)
        {
            var kljuc = new object[] { int.Parse(modelUI.Id) };
            DobavljacDrveta modelDB = _repozitorijum.Get(kljuc);
            IzUIModelUModelBezKljuca(modelDB, modelUI);
            _repozitorijum.Update(modelDB, kljuc);
        }

        public void Obrisi(int id)
        {
            _repozitorijum.Remove(new object[] { id });
        }

        private void IzUIModelUModel(DobavljacDrveta modelDB, DobavljacDrvetaModel modelUI)
        {
            modelDB.Id = int.Parse(modelUI.Id);
            modelDB.Naziv = modelUI.Naziv;
            modelDB.Adresa = AdresaUPunOblik(modelUI.Mesto, modelUI.Ulica, modelUI.Broj);
        }

        private void IzModelUUIModel(DobavljacDrveta modelDB, DobavljacDrvetaModel modelUI)
        {
            modelUI.Id = modelDB.Id.ToString();
            modelUI.Naziv = modelDB.Naziv;
            AdresaUSkracenOblik(modelDB.Adresa, out string mesto, out string ulica, out string broj);
            modelUI.Mesto = mesto;
            modelUI.Ulica = ulica;
            modelUI.Broj = broj;
        }

        private void IzUIModelUModelBezKljuca(DobavljacDrveta modelDB, DobavljacDrvetaModel modelUI)
        {
            modelDB.Naziv = modelUI.Naziv;
            modelDB.Adresa = AdresaUPunOblik(modelUI.Mesto, modelUI.Ulica, modelUI.Broj);
        }

        private string AdresaUPunOblik(string mesto, string ulica, string broj)
        {
            return mesto + "|" + ulica + "|" + broj;
        }

        private void AdresaUSkracenOblik(string ulaznaAdresa, out string mesto, out string ulica, out string broj)
        {
            string[] delovi = ulaznaAdresa.Split('|');
            mesto = delovi[0];
            ulica = delovi[1];
            broj = delovi[2];
        }
    }
}