using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    public class Faktura
    {
        public Bokning Bokning;

        public Expidit AnstNr;
        public DateTime FaktiskÅterTid { get; set; }
        public int TotalPris { get; set; }
        public Faktura(Bokning bokning, Expidit anstNr, DateTime faktiskÅterTid, int totalPris)
        {
            Bokning = bokning;
            AnstNr = anstNr;
            FaktiskÅterTid = faktiskÅterTid;
            TotalPris = totalPris;
        }
    }
}