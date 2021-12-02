using BazaPodataka.Model.Authentication;
using KorisnickiInterfejs.Servisi.Auth;
using PristupBaziPodataka.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KorisnickiInterfejs.Servisi
{
    public class AuthorizationService
    {
        public static void Authorize(List<TipKorisnika> allowedUserTypes)
        {
            Korisnik korisnik = AuthenticateService.Instance.TrenutniKorisnik;
            if (!allowedUserTypes.Contains(GetUserType(korisnik)))
                throw new UnauthorizedAccessException("Nemate prava pristupa selektovanoj funkcionalnosti.");
        }

        public static bool IsAuthorized(List<TipKorisnika> allowedUserTypes)
        {
            try
            {
                Authorize(allowedUserTypes);
                return true;
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private static TipKorisnika GetUserType(Korisnik korisnik)
        {
            if (korisnik is AdministratorKorisnik)
                return TipKorisnika.Admin;
            if (korisnik is SegrtKorisnik)
                return TipKorisnika.Segrt;
            if (korisnik is StolarKorisnik)
                return TipKorisnika.Stolar;
            if (korisnik is MagacionerKorisnik)
                return TipKorisnika.Magacioner;
            throw new Exception("Invalid user.");
        }
    }
}
