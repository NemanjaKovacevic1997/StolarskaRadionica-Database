using BazaPodataka.Model.Authentication;
using PristupBaziPodataka;
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
        private Repository<Korisnik> _repo;

        private AuthenticateService()
        {
            TrenutniKorisnik = null;
            _repo = new Repository<Korisnik>();
        }

        public Korisnik Login(string korisnickoIme, string lozinka)
        {
            TrenutniKorisnik = _repo.Get(korisnickoIme);
            return TrenutniKorisnik != null && TrenutniKorisnik.Lozinka == lozinka ? TrenutniKorisnik : null;
        }

        public Korisnik Register(Korisnik korisnik)
        {
            Korisnik kor = _repo.Get(korisnik.KorisnickoIme);
            if (kor != null)
                return null;
            _repo.Add(korisnik);
            return korisnik;
        }
    }
}
