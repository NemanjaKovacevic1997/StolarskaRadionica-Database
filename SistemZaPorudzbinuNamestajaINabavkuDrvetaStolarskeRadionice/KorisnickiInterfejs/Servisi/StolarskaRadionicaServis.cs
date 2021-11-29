using BazaPodataka.Model;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using PristupBaziPodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Servisi
{
    public class StolarskaRadionicaServis
    {
        private static StolarskaRadionicaServis _instance = null;

        public static StolarskaRadionicaServis Instance
        {
            get 
            {
                if (_instance == null)
                    _instance = new StolarskaRadionicaServis();
                return _instance;
            }
        }

        private Repository<StolarskaRadionica> _repozitorijum = new Repository<StolarskaRadionica>();

        public void Dodaj(StolarskaRadionicaModel stolarskaRadionica)
        {
            StolarskaRadionica radionica = new StolarskaRadionica();
            IzUIModelUModel(radionica, stolarskaRadionica);
            _repozitorijum.Add(radionica);
        }

        public StolarskaRadionicaModel Citaj(string id)
        {
            StolarskaRadionica stolarskaRadionica = new StolarskaRadionica();

            if (!int.TryParse(id, out int Id))
                return null;

            stolarskaRadionica = _repozitorijum.Get(Id);

            if(stolarskaRadionica != null)
            {
                StolarskaRadionicaModel model = new StolarskaRadionicaModel();
                IzModelUUIModel(stolarskaRadionica, model);
                return model;
            }

            return null;
        }

        public ICollection<StolarskaRadionicaModel> CitajSve()
        {
            ICollection<StolarskaRadionicaModel> stolarskaRadionicaModeli = new List<StolarskaRadionicaModel>();

            var stolarskeRadionice = _repozitorijum.GetAll();

            if (stolarskeRadionice == null)
                return null;

            StolarskaRadionicaModel model;
            foreach (var sr in stolarskeRadionice)
            {
                model = new StolarskaRadionicaModel();
                IzModelUUIModel(sr, model);
                stolarskaRadionicaModeli.Add(model);
            }

            return stolarskaRadionicaModeli;
        }

        public void Izmeni(StolarskaRadionicaModel stolarskaRadionica)
        {
            var kljuc = new object[] { int.Parse(stolarskaRadionica.Id)};
            StolarskaRadionica modelDB = _repozitorijum.Get(kljuc);

            IzUIModelUModelBezKljuca(modelDB, stolarskaRadionica);
            _repozitorijum.Update(modelDB, kljuc);
        }

        public void Obrisi(string id)
        {
            if (int.TryParse(id, out int Id))
                _repozitorijum.Remove(Id);
        }

        private string AdresaUPunOblik(string mesto, string ulica, string broj)
        {
            return mesto + "|" + ulica + "|" + broj;
        }

        private void AdresaUSkracenOblik(string ulaznaAdresa, out string mesto, out string ulica, out string broj)
        {
            string [] delovi = ulaznaAdresa.Split('|');
            mesto = delovi[0];
            ulica = delovi[1];
            broj = delovi[2];
        }

        private void IzUIModelUModel(StolarskaRadionica stolarskaRadionica, StolarskaRadionicaModel stolarskaRadionicaModel)
        {
            stolarskaRadionica.Id = int.Parse(stolarskaRadionicaModel.Id);
            stolarskaRadionica.Naziv = stolarskaRadionicaModel.Naziv;
            stolarskaRadionica.Adresa = AdresaUPunOblik(stolarskaRadionicaModel.Mesto, stolarskaRadionicaModel.Ulica, stolarskaRadionicaModel.Broj);
        }
        private void IzUIModelUModelBezKljuca(StolarskaRadionica stolarskaRadionica, StolarskaRadionicaModel stolarskaRadionicaModel)
        {
            stolarskaRadionica.Naziv = stolarskaRadionicaModel.Naziv;
            stolarskaRadionica.Adresa = AdresaUPunOblik(stolarskaRadionicaModel.Mesto, stolarskaRadionicaModel.Ulica, stolarskaRadionicaModel.Broj);
        }

        private void IzModelUUIModel(StolarskaRadionica stolarskaRadionica, StolarskaRadionicaModel stolarskaRadionicaModel)
        {
            stolarskaRadionicaModel.Id = stolarskaRadionica.Id.ToString();
            stolarskaRadionicaModel.Naziv = stolarskaRadionica.Naziv;
            AdresaUSkracenOblik(stolarskaRadionica.Adresa, out string mesto, out string ulica, out string broj);
            stolarskaRadionicaModel.Mesto = mesto;
            stolarskaRadionicaModel.Ulica = ulica;
            stolarskaRadionicaModel.Broj = broj;
        }
    }
}
