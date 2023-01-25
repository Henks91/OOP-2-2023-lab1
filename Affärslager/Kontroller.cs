using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public Bokning SkapaBokning(int boknr, Expidit expidit, Medlem medlem, DateTime utTid, DateTime återTid, DateTime faktiskUtTid, List<Bok> bokadeBöcker)
        {
            Bokning bokning = new Bokning(boknr, expidit, medlem, utTid, återTid,  faktiskUtTid, bokadeBöcker);
            unitOfWork.BokningRepository.Add(bokning);
            return bokning;
            
        }

        public Medlem Hittamedlem(int medlemNr)
        {
            Medlem medlem = unitOfWork.MedlemRepository.FirstOrDefault(e => e.MedlemsNr == medlemNr);
            if (medlem != null)
            {
                medlemNr = medlem.MedlemsNr;
            }
            return medlem;
        }

        public void BokTillBokning(List<Bok> boks)
        {
            IList<Bok> bokadeBöcker = new List<Bok>();

                foreach (Bok b in boks)
                {
                    bokadeBöcker.Add(b);
                    b.Bokad();
                }           
            //return bokadeBöcker;           
        }

        //public List<Sal> HämtaTillgängligaSalar(DateTime från, DateTime till)
        //{
        //    List<Sal> otillgängligaGrupprum = new List<Sal>();

        //    foreach (Schema schema in schema)
        //    {
        //        foreach (SchemaRad rad in schema.Schemarader)
        //        {
        //            if ((från >= rad.Från && från < rad.Till) || (till > rad.Från && till <= rad.Till))
        //                otillgängligaGrupprum.Add(rad.Sal);
        //        }
        //    }

        //    return salar.Where(rum => !otillgängligaGrupprum.Contains(rum)).ToList();
        //}

        public List<Bokning> TidsKontroll(DateTime från, DateTime till)
        {
            List<Bokning> BokadeBöcker = new List<Bokning>();
            if (Bokning b in unitOfWork.BokningRepository.Find(från => b.ÅterTid && till)
            {

            }  
            return unitOfWork.BokningRepository.
        }
        public void Jämförtid(DateTime Tid, Bokning bokn)
        {
            IEnumerable<Bokning> b = unitOfWork.BokningRepository.Find(b => b.ÅterTid > Tid);
            if (bokn.ÅterTid > Tid)
            {

            }
        }

        public Bok HittaBok(string boktitel)
        {
            Bok bok = unitOfWork.BokRepository.FirstOrDefault(bk => bk.Titel == boktitel);

            //Bokning b = unitOfWork.BokningRepository.Find(b => b.ÅterTid < tid);
            if (bok.Titel != null && bok.Status == true)
            {
                bok.Titel = boktitel;
            }
            return bok;
        }

        public Bok HittaBok(string boktitel)
        {
            Bok bok = unitOfWork.BokRepository.FirstOrDefault(bk => bk.Titel == boktitel);
            if (bok.Titel != null)
            {
                bok.Titel = boktitel;
            }
            return bok;

            //public IList<Bokning> VisaBokning(Bokning bobo) // funkar ej, lyckades ej på denna front
            //{

            //    Bokning dinBokning = unitOfWork.BokningRepository.FirstOrDefault(db => db.BokningsNr == bobo.BokningsNr || db.Medlem.MedlemsNr == bobo.Medlem.MedlemsNr);

            //    if (dinBokning != null && dinBokning.BokningsNr == bobo.BokningsNr)
            //    {
            //        bobo.BokningsNr = dinBokning.BokningsNr;

            //    }
            //    else if (dinBokning != null && dinBokning.Medlem.MedlemsNr == bobo.BokningsNr)
            //    {
            //        bobo.Medlem.MedlemsNr = dinBokning.Medlem.MedlemsNr;
            //    }
            //    return dinBokning as IList<Bokning>; // waaw

            //}


            public Bokning VisaBokning(int bob) // accepterade förlusten efter 2h - Denna funkar.
        {

            IList<Bokning> boknings = new List<Bokning>(); // denna kan möjligtvis ändras från IList till IEnumerable
            Bokning dinBokning = unitOfWork.BokningRepository.FirstOrDefault(dinBokning => dinBokning.BokningsNr == bob || dinBokning.Medlem.MedlemsNr == bob);
            boknings.Where(db => db.BokningsNr == bob || db.Medlem.MedlemsNr == bob); 
            if (dinBokning != null && dinBokning.BokningsNr == bob)
            {
                bob = dinBokning.BokningsNr;

            }
            else if (dinBokning != null && dinBokning.Medlem.MedlemsNr == bob)
            {
                bob = dinBokning.Medlem.MedlemsNr;
            }
            return dinBokning; 

        }


        //public Bokning BokaBok()
        //{

        //    Bokning bo = new Bokning();
        //    unitOfWork.BokningRepository.Add(bo);
        //    LoggedIn.Reserved = r;
        //    unitOfWork.Save();
        //    return r;
        //}


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
    }




}
