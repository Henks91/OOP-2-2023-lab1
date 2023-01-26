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
        private UnitOfWork unitOfWork;
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

        public Bokning SkapaBokning(Medlem medlem, DateTime utTid, List<Bok> bokadeBöcker)
        {
            DateTime faktiskUtTid = default(DateTime);
            DateTime återTid = default(DateTime);
            Bokning bokning = new Bokning(Autentisering, medlem, utTid, återTid,  faktiskUtTid, bokadeBöcker, false);
            unitOfWork.BokningRepository.Add(bokning);
            unitOfWork.Save();
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
            unitOfWork.Save();
            return faktura;
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

        public Bokning VisaBokning(int bob) 
        {
            IList<Bokning> boknings = new List<Bokning>(); 
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
    }
}
