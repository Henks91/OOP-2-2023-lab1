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
            foreach (Bok b in unitOfWork.BokRepository.Find(b => b.ÄrTillgänglig == true))
            {
                böcker.Add(b);
            }
            return böcker;
        }

        public Bokning SkapaBokning(Medlem medlem, DateTime utTid, DateTime återTid, DateTime faktiskUtTid, List<Bok> bokadeBöcker) //se över expidiit i ctor
        {
            Bokning bokning = new Bokning(Autentisering, medlem, utTid, återTid,  faktiskUtTid, bokadeBöcker, false);
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

        public Faktura SkapaFaktura(Bokning bokning) 
        {

            Faktura faktura = new Faktura(bokning, Autentisering, DateTime.Now);
            if (faktura.TotalPris <0)
            {
                faktura.TotalPris = 0;
            }
            unitOfWork.FakturaRepository.Add(faktura);
            return faktura;
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

        public Bok HittaBok(string boktitel)
        {
            Bok bok = unitOfWork.BokRepository.FirstOrDefault(bk => bk.Titel.ToLower() == boktitel.ToLower());
            if (bok.Titel.ToLower() != null)
            {
                bok.Titel = boktitel;
            }
            return bok;
        }



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
