using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Servisi;
using PristupBaziPodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.ViewModels
{
    public class FunkcijeViewModel : GenericCRUDViewModel<ReturnTable>
    {
        private FabrikaPoruka _fabrikaPoruka = new FabrikaPoruka();
        FunctionAccess functionAccess = new FunctionAccess();

        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                if (!ValidacijaId(id))
                    ValidacionaGreska = _fabrikaPoruka.Uzmi("Id");
            }
        }

        public bool CanIzmeni(string id)
        {
            if (ValidacijaId(id))
                return true;
            return false;
        }

        public void Izmeni()
        {
            var sr = StolarskaRadionicaServis.Instance.Citaj(id);
            if (sr == null)
            {
                ValidacionaGreska = "Stolarska radionica sa datim id-jem ne postoji.";
                return;
            }
            
            Kolekcija = ListConverter<ReturnTable>.KonvertujListu(functionAccess.PronadjiPonude(int.Parse(Id)));
        }

        private bool ValidacijaId(string str)
        {
            if (string.IsNullOrEmpty(str) || !int.TryParse(str, out int number))
                return false;

            if (number <= 0)
                return false;

            return true;
        }
    }
}