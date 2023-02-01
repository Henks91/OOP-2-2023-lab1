using System;

namespace Entiteter
{
    public class Faktura
    {
        public Bokning Bokning;

        public Expidit Expidit;
        public DateTime FaktiskÅterTid { get; set; }
        public int TotalPris { get; set; }
        public int DagsKostnad = 10;
        public Faktura(Bokning bokning, Expidit expidit, DateTime faktiskÅterTid, int antalBöcker)
        {
            Bokning = bokning;
            Expidit = expidit;
            FaktiskÅterTid = faktiskÅterTid;
            TotalPris = (int)((faktiskÅterTid - Bokning.ÅterTid).TotalDays) * (DagsKostnad * antalBöcker);
        }
    }
}