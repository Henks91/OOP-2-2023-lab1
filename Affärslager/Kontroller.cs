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

        public void SkapaBokning(int bokningsNr, Expidit expidit, Medlem medlem, DateTime utTid, DateTime faktiskUtTid, DateTime återTid)
        {
            Bokning bokning = new Bokning(bokningsNr, expidit, medlem, utTid, faktiskUtTid, återTid);
            unitOfWork.BokningRepository.Add(bokning);
        }

        public Bokning BokaBok()
        {
            b.Status = false;
            Bokning bo = new Bokning();
            unitOfWork.BokningRepository.Add(bo);
            LoggedIn.Reserved = r;
            unitOfWork.Save();

            return r;
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
