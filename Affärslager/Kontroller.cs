using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datalager;
using Entiteter;



namespace Affärslager
{
    public class Kontroller
    {
        public Expidit Autentisering
        {
            get; private set;
        }        

        public bool Inloggning(int anstNr, string lösenord)
        {
            unitOfWork = new UnitOfWork();
            Expidit expidit = unitOfWork.ExpiditRepository.FirstOrDefault(e => e.AnstNr == anstNr);
            if (expidit != null && expidit.Lösenordskontroll(lösenord))
            {
                Autentisering = expidit;
                return true;
            }
            Autentisering = null;
            return false;
        }

        public IList<Bok> HämtaTillgängligaBöcker()
        {
            List<Bok> böcker = new List<Bok>();
            foreach (Bok b in unitOfWork.BokRepository.Find(b => b.Status == true))
            {
                böcker.Add(b);
            }
            return böcker;
        }

        /*public void SkapaBokning(int bokningsNr, Expidit expidit, Medlem medlem, DateTime utTid, DateTime faktiskUtTid, DateTime återTid)
        {
            Bokning bokning = new Bokning(bokningsNr, expidit, medlem, utTid, faktiskUtTid, återTid);
            unitOfWork.BokningRepository.Add(bokning);
        }*/

        public Bokning Bokning1(Bok bok, Medlem medlem, List<Bok> böcker)
        {
            
           

            Random rnd = new Random(); //skapar möjligheten att lägga ett randomnummer
            int bokningsNr = rnd.Next(1,9999); //lägger till ett randomnummer
            Bokning bokning = new Bokning(bokningsNr,Autentisering,medlem,böcker, DateTime.Now, DateTime.Now.AddDays(+7), DateTime.Now.AddDays(+14));
            unitOfWork.BokningRepository.Add(bokning);

            foreach (Bok b in böcker)
            {
                bok.Bokad();
            }

            unitOfWork.Save();
            return bokning;
        }
        public Bokning Boka(Expidit autentisering, Medlem medlem, List<Bok> böckerSomSkaBokas )
        {
            int bokningsNr = 0;
            Bokning bokning = new Bokning(bokningsNr,Autentisering,medlem, böckerSomSkaBokas, DateTime.Now, DateTime.Now.AddDays(+7), DateTime.Now.AddDays(+14));
            unitOfWork.BokningRepository.Add(bokning);

            foreach (Bok bok in böckerSomSkaBokas)
            {
                bok.Bokad();
            }
            return bokning;
            unitOfWork.Save();

            
        }


        // Kolla patriks kod för rumsbokning - Sax

        //public List<TillgängligaBöcker> VisaTillgängligaBöcker() // Böcker som kan bli lånade
        //{
        //    return new List<TillgängligaBöcker>().ToString;
        //}
        //public List<BokadeBöcker> VisaBokadeBöcker() // Lista där bokade böcker läggs vid bokning - innan utlämning
        //{
        //    return new List<BokadeBöcker>().ToString;
        //}

        //public List<UtlånadeBöcker> VisaUtlånadeBöcker()  // Behövs troligen inte då hela bokningen cancelleras
        //{
        //    return new List<VisaUtlånadeBöcker>().ToString;
        //}

        //public Expidit SkapaInlogg(int anstNr, string lösenord) // Ska flyttas till repository för inlogg
        //{
        //    return databas.SkapaInLogg(anstNr, lösenord);
        //}

        //public Expidit SkapaInLogg(string anstNr, string lösenord) // Om vi har en ny klass som är "autentiserade" för att logga in skulle vi lägga till expediter i den
        //{
        //    if (Expediter.Find(a => a.AnstNr == anstNr) != null)
        //        throw new Exception($"Expedit med {anstNr} har redan inlogg.");

        //    Expidit expediten = new Expidit(anstNr, lösenord);
        //    Expediter.Add(expediten);
        //    return expediten;
        //}

        //public potatis()
        //{
        //    List<Bokning> bokningar = kontext.BokningKontroller.HämtaBokningar(kontext.Session.Användare); // Kan behöva vid val av datum innan bokning

        //    {
        //        Console.WriteLine($"Bokningnummer: {bokning.Bokningnummer}");
        //        foreach (Bokningrad rad in bokning.Bokningrader)
        //        {
        //            Console.WriteLine("Rumnummer: {0}\tFrån: {1}\tTill: {2}",
        //                rad.Grupprum.Rumnummer,
        //                rad.Från.ToString("yyyy-MM-dd HH:mm"),
        //                rad.Till.ToString("yyyy-MM-dd HH:mm"));
        //        }
        //    }
        //}

        /// <summary>
        ///  The LogIn system operation.
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// 
        private UnitOfWork unitOfWork;
        private Repository<Bok> repository;
    }




}
