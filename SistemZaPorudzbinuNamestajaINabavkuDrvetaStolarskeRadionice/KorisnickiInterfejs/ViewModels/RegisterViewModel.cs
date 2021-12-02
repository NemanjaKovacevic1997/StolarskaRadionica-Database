using Caliburn.Micro;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi;
using KorisnickiInterfejs.Servisi.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KorisnickiInterfejs.ViewModels
{
    public class RegisterViewModel : Screen
    {
        IWindowManager Manager = new WindowManager();
        private AuthenticateModel modelUI = new AuthenticateModel();
        public string KorisnickoIme
        {
            get { return modelUI.KorisnickoIme; }
            set
            {
                modelUI.KorisnickoIme = value;
            }
        }

        public string Lozinka
        {
            get { return modelUI.Lozinka; }
            set
            {
                modelUI.Lozinka = value;
            }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
            }
        }

        private bool _isStolar;
        public bool IsStolar
        {
            get { return _isStolar; }
            set
            {
                _isStolar = value;
            }
        }


        private bool _isSegrt;
        public bool IsSegrt
        {
            get { return _isSegrt; }
            set
            {
                _isSegrt = value;
            }
        }


        private bool _isMagacioner;
        public bool IsMagacioner
        {
            get { return _isMagacioner; }
            set
            {
                _isMagacioner = value;
            }
        }

        private string _validacionaGreska = "";

        public string ValidacionaGreska
        {
            get { return _validacionaGreska; }
            set
            {
                _validacionaGreska = value;
                NotifyOfPropertyChange(() => ValidacionaGreska);
            }
        }


        public void Register()
        {
            TipKorisnika tipKorisnika;
            try
            {
                tipKorisnika = DetermineTipKorsnika();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
           
            AuthenticateModel model = new AuthenticateModel(this.KorisnickoIme, this.Lozinka);
            try
            {
                AuthenticateService.Instance.Register(model, tipKorisnika);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            MessageBox.Show($"Uspesno registrovan korisnik {KorisnickoIme}");
            TryClose();
        }


        public TipKorisnika DetermineTipKorsnika()
        {
            if (this.IsAdmin)
                return TipKorisnika.Admin;
            if (this.IsStolar)
                return TipKorisnika.Stolar;
            if (this.IsSegrt)
                return TipKorisnika.Segrt;
            if (this.IsMagacioner)
                return TipKorisnika.Magacioner;
            throw new Exception("Tip korisnika nije selektovan.");
        }
    }
}
