using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KorisnickiInterfejs.ViewModels
{
    public class PocetniViewModel : Screen
    {
        IWindowManager manager = new WindowManager();

        public void PrikaziStolarskaRadionica()
        {
            try
            {
                manager.ShowWindow(new StolarskaRadionicaViewModel());
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void PrikaziMusterija()
        {
            manager.ShowWindow(new MusterijaViewModel());
        }

        public void PrikaziPoruzbina()
        {
            manager.ShowWindow(new PorudzbinaViewModel());
        }

        public void PrikaziStavkaPoruzbine()
        {
            manager.ShowWindow(new StavkaPorudzbineViewModel());
        }

        public void PrikaziCenovnik()
        {
            manager.ShowWindow(new CenovnikViewModel());
        }

        public void PrikaziStavkaCenovnika()
        {
            manager.ShowWindow(new StavkaCenovnikaViewModel());
        }

        public void PrikaziVrstaNamestaja()
        {
            manager.ShowWindow(new VrstaNamestajaViewModel());           
        }

        public void PrikaziRadnik()
        {
            manager.ShowWindow(new StolarskaRadionicaViewModel());
        }

        public void PrikaziSegrt()
        {
            manager.ShowWindow(new SegrtViewModel());
        }

        public void PrikaziMacioner()
        {
            manager.ShowWindow(new MagacionerViewModel());
        }

        public void PrikaziStolar()
        {
            manager.ShowWindow(new StolarViewModel());
        }

        public void PrikaziZahtev()
        {
            manager.ShowWindow(new ZahtevViewModel());
        }

        public void PrikaziVrstaDrveta()
        {
            manager.ShowWindow(new VrstaDrvetaViewModel());
        }

        public void PrikaziDobavljacDrveta()
        {
            manager.ShowWindow(new DobavljacDrvetaViewModel());
        }

        public void PrikaziMagacin()
        {
            manager.ShowWindow(new MagacinViewModel());
        }

        public void PrikaziFunkcije()
        {
            manager.ShowWindow(new FunkcijeViewModel());
        }
    }
}
