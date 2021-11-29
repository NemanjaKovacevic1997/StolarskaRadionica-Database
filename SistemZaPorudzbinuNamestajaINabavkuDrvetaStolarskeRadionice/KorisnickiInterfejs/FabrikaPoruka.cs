using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs
{
    public class FabrikaPoruka
    {
        public string Uzmi(string tip)
        {
            string poruka = "";

            switch (tip)
            {
                case "Id": poruka = "Id mora biti pozitivan, jedinstven i ceo broj."; break;
                case "Naziv": poruka = "Naziv sadrzaj moze biti proizvoljan ali ne duzi od 30 kar."; break;
                case "Mesto": poruka = "Mesto sadrzi iskljucivo slova i ne sme biti duze od 30 kar."; break;
                case "Broj": poruka = "Broj mora biti pozitivan, nenula, jedinstven i ispod 5 cifara."; break;
                case "Ulica": poruka = "Ulica sadrzi iskljucivo slova i mora imati ispod 30 kar."; break;
                case "Ime": poruka = "Ime sadrzi iskljucivo slova i mora imati ispod 30 kar."; break;
                case "Prezime": poruka = "Prezime sadrzi iskljucivo slova i mora imati ispod 30 kar."; break;
                case "Jmbg": poruka = "Jmbg sadrzi iskljucivo cifre i mora ih imati tacno 13."; break;
                case "Ocena": poruka = "Ocena mora biti ceo broj izmedju 1 i 10."; break;
                case "Cena": poruka = "Cena mora biti pozitivan broj sa manje od 10 cifara."; break;
                case "RedniBroj": poruka = "Redni broj mora biti pozitivan, sa manje od 10 cifara."; break;
                case "Kolicina": poruka = "Kolicina mora biti pozitivan broj sa manje od 7 cifara."; break;
                case "Materijal": poruka = "Materijal sadrzi iskljucivo slova i mora imati ispod 30 kar."; break;
                default: poruka = "Greska."; break;
            }

            return poruka;
        }
    }
}
