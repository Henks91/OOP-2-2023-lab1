using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    class Faktura
    {
        public Bokning BokningsNr;

        public Expidit AnstNr;
        public DateTime FaktiskÅterTid { get; set; }
        public int TotalPris { get; set; }
        public Faktura(Bokning bokningsNr, Expidit anstNr, DateTime faktiskÅterTid, int totalPris)
        {
            BokningsNr = bokningsNr;
            AnstNr = anstNr;
            FaktiskÅterTid = faktiskÅterTid;
            TotalPris = totalPris;
        }
    }
}