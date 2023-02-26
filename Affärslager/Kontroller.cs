﻿using Datalager;
using Entiteter;
using System;
using System.Collections.Generic;
using System.Linq;



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
        public List<Bok> HämtaTillgängligaBöcker() //metod för att lista alla tillgängliga böcker som ligger i BokRepository
        {
            List<Bok> böcker = unitOfWork.BokRepository.Find(b => b.ÄrTillgänglig == true)
                .ToList();
            return böcker;
        }
        public Bokning SkapaBokning(Medlem medlem, DateTime startLån, List<Bok> bokadeBöcker)  //metod för att instansiera en bokning
        {
            DateTime faktiskstartLån = default(DateTime); //Dessa värden sätts som default eftersom de tilldelas ett värde senare för uthämtning av bok, därav propertyns public synlighet
            DateTime återTid = default(DateTime);         //Dessa värden sätts som default eftersom de tilldelas ett värde senare för uthämtning av bok, därav propertyns public synlighet
            Bokning bokning = new Bokning(Autentisering, medlem, startLån, återTid, faktiskstartLån, bokadeBöcker, false, false);          
            foreach (Bok b in bokadeBöcker) // loop för att ändra bokens status från true (tillgänglig) till false (bokad)
            {
                b.Bokad();
            }
            unitOfWork.BokningRepository.Add(bokning);            
            unitOfWork.Save();
            return bokning;
        }
        public Medlem Hittamedlem(int medlemNr)
        {
            Medlem medlem = unitOfWork.MedlemRepository.FirstOrDefault(e => e.MedlemsNr == medlemNr);
            
            return medlem;
        }
        public Faktura SkapaFaktura(Bokning bokning)
        {
            int antalBöcker = bokning.BokadeBöcker.Count();
            Faktura faktura = new Faktura(bokning, Autentisering, DateTime.Now, antalBöcker);
            if (faktura.TotalPris < 0)
            {
                faktura.TotalPris = 0;
            }
            unitOfWork.FakturaRepository.Add(faktura);
            unitOfWork.Save();

            return faktura;
        }
        public Bok HittaBok(string boktitel)
        {
            Bok bok = unitOfWork.BokRepository.FirstOrDefault(bk => bk.Titel.ToLower() == boktitel.ToLower() && bk.ÄrTillgänglig == true);
            
            return bok;
        }
        public Bokning UtlämningAvBöcker(int bNr)
        {
            Bokning dinBokning = unitOfWork.BokningRepository.FirstOrDefault(dinBokning => dinBokning.BokningsNr == bNr || dinBokning.Medlem.MedlemsNr == bNr);
            if (dinBokning.UppHämtad == true)
            {
                return null;
            }
            dinBokning.FaktisktStartLån = dinBokning.StartLån; //här kan man köra datetime.now här istället för att göra det mer realistiskt men inte aktuellt för detta program
            dinBokning.ÅterTid = dinBokning.FaktisktStartLån.AddDays(+14);
            dinBokning.Upphämtad();
            return dinBokning;
        }
        public Bokning LämnaTillbakaBok(int bNr)
        {
            Bokning dinBokning = unitOfWork.BokningRepository.FirstOrDefault(dinBokning => dinBokning.BokningsNr == bNr || dinBokning.Medlem.MedlemsNr == bNr);
            if (dinBokning.Återlämnad == true || dinBokning.UppHämtad == false)
            {
                return null;
            }
            foreach (Bok b in dinBokning.BokadeBöcker)
            {
                b.Tillgänglig();
            }
            dinBokning.ÅterlämnadBokning();
            return dinBokning;
        }
    }
}
