using BazaPodataka.Model.Authentication;
using KorisnickiInterfejs.Models;
using KorisnickiInterfejs.Servisi.Auth;
using KorisnickiInterfejs.Servisi.Auth.PasswordSecurity;
using PristupBaziPodataka;
using PristupBaziPodataka.Model.Authentication;
using System;


namespace KorisnickiInterfejs.Servisi
{
    public class AuthenticateService
    {
        private static AuthenticateService _instance = null;

        public static AuthenticateService Instance
        {
            get 
            {
                if (_instance == null)
                    _instance = new AuthenticateService();
                return _instance;
            }
        }

        public Korisnik TrenutniKorisnik { get; private set; }
        private Repository<Korisnik> _korisnikRepository;
        private Repository<AdministratorKorisnik> _adminKorisnikRepository;
        private Repository<StolarKorisnik> _stolarKorisnikRepository;
        private Repository<SegrtKorisnik> _segrtKorisnikRepository;
        private Repository<MagacionerKorisnik> _magacionerKorisnikRepository;

        private AuthenticateService()
        {
            TrenutniKorisnik = null;
            _korisnikRepository = new Repository<Korisnik>();
            _adminKorisnikRepository = new Repository<AdministratorKorisnik>();
            _stolarKorisnikRepository = new Repository<StolarKorisnik>();
            _segrtKorisnikRepository = new Repository<SegrtKorisnik>();
            _magacionerKorisnikRepository = new Repository<MagacionerKorisnik>();
        }

        public Korisnik Login(string korisnickoIme, string lozinka)
        {
            TrenutniKorisnik = _korisnikRepository.Get(korisnickoIme);
            if (TrenutniKorisnik == null)
                return null;

            bool isPasswordValid = PasswordStorage.VerifyPassword(lozinka, TrenutniKorisnik.Lozinka);
            return isPasswordValid ? TrenutniKorisnik : null;
        }

        public void Register(AuthenticateModel authenticateModel, TipKorisnika tipKorisnika)
        {
            Korisnik kor = _korisnikRepository.Get(authenticateModel.KorisnickoIme);
            if (kor != null)
                throw new Exception("Korisnik pod unetim korisnickim imenom vec postoji u bazi podataka.");

            string hashedPasword = PasswordStorage.CreateHash(authenticateModel.Lozinka);

            if (tipKorisnika == TipKorisnika.Stolar)
            {
                StolarKorisnik korisnik = new StolarKorisnik() { KorisnickoIme = authenticateModel.KorisnickoIme, Lozinka = hashedPasword };
                _stolarKorisnikRepository.Add(korisnik);
            }
            else if (tipKorisnika == TipKorisnika.Admin)
            {
                AdministratorKorisnik korisnik = new AdministratorKorisnik() { KorisnickoIme = authenticateModel.KorisnickoIme, Lozinka = hashedPasword };
                _adminKorisnikRepository.Add(korisnik);
            }
            else if (tipKorisnika == TipKorisnika.Segrt)
            {
                SegrtKorisnik korisnik = new SegrtKorisnik() { KorisnickoIme = authenticateModel.KorisnickoIme, Lozinka = hashedPasword };
                _segrtKorisnikRepository.Add(korisnik);
            }
            else if (tipKorisnika == TipKorisnika.Magacioner)
            {
                MagacionerKorisnik korisnik = new MagacionerKorisnik() { KorisnickoIme = authenticateModel.KorisnickoIme, Lozinka = hashedPasword };
                _magacionerKorisnikRepository.Add(korisnik);
            }
        }
    }
}
