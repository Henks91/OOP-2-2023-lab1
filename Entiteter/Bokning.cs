using Entiteter.Interface;
using System;
using System.Collections.Generic;

namespace Entiteter
{
    public class Bokning : IBokning
    {
        private static int _BokningsNr = 0;
        private int bokningsNr = 0;
        public int BokningsNr { get { return bokningsNr; } }

        public Expidit Expidit { get; private set; } 

        public Medlem Medlem { get; private set; }

        public IList<Bok> BokadeBöcker;
        public DateTime StartLån { get; private set; }  // Det datum som skrivs in när man först planerar att boka boken ifrån i menyval 1
        public DateTime FaktisktStartLån { get; set; } //När boken faktiskt blev upphämtad av medlem, tex om upphämtning sker någon dag efter önskat startdatum på bokningen som angavs under menyval 1.
        public DateTime ÅterTid { get; set; } //När boken ska vara tillbaka lämnad, 14 dagar efter startdatum för bokningen
        public bool UppHämtad { get; set; }
        public bool Återlämnad { get; set; }

        public Bokning(Expidit expidit, Medlem medlem, DateTime startLån, DateTime återTid, DateTime faktiskStartLån, IList<Bok> böcker, bool upphämtad, bool återlämnad)
        {
            _BokningsNr++;
            this.bokningsNr = _BokningsNr;
            Medlem = medlem;
            Expidit = expidit;
            BokadeBöcker = böcker;
            StartLån = startLån;
            FaktisktStartLån = faktiskStartLån;
            ÅterTid = återTid;
            UppHämtad = upphämtad;
            Återlämnad = återlämnad;
        }
        public void Upphämtad()
        {
            UppHämtad = true;
        }
        public void Återlämna()
        {
            Återlämnad = true;
        }
    }
}
