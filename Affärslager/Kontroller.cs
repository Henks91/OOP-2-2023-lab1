using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affärslager
{
    public class Kontroller
    {
        // Kolla patriks kod för rumsbokning - Sax

        public List<TillgängligaBöcker> VisaTillgängligaBöcker() // Böcker som kan bli lånade
        {
            return new List<TillgängligaBöcker>().ToString;
        }
        public List<BokadeBöcker> VisaBokadeBöcker() // Lista där bokade böcker läggs vid bokning - innan utlämning
        {
            return new List<BokadeBöcker>().ToString;
        }

        public List<UtlånadeBöcker> VisaUtlånadeBöcker()  // Behövs troligen inte då hela bokningen cancelleras
        {
            return new List<VisaUtlånadeBöcker>().ToString;
        }

        public Expedit SkapaInlogg(int anstNr, string lösenord) // Ska flyttas till repository för inlogg
        {
            return databas.SkapaInLogg(anstNr, lösenord);
        }

        public Expedit SkapaInLogg(string anstNr, string lösenord) // Om vi har en ny klass som är "autentiserade" för att logga in skulle vi lägga till expediter i den
        {
            if (Expediter.Find(a => a.AnstNr == anstNr) != null)
                throw new Exception($"Expedit med {anstNr} har redan inlogg.");

            Expedit expediten = new Expedit(anstNr, lösenord);
            Expediter.Add(expediten);
            return expediten;
        }

        public potatis()
        {
            List<Bokning> bokningar = kontext.BokningKontroller.HämtaBokningar(kontext.Session.Användare); // Kan behöva vid val av datum innan bokning

            {
                Console.WriteLine($"Bokningnummer: {bokning.Bokningnummer}");
                foreach (Bokningrad rad in bokning.Bokningrader)
                {
                    Console.WriteLine("Rumnummer: {0}\tFrån: {1}\tTill: {2}",
                        rad.Grupprum.Rumnummer,
                        rad.Från.ToString("yyyy-MM-dd HH:mm"),
                        rad.Till.ToString("yyyy-MM-dd HH:mm"));
                }
            }
        }
    }


}
