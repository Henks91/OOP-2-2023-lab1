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

        public Expidit Expidit { get; private set; } //fundera ´kring public här

        public Medlem Medlem { get; private set; }

        public IList<Bok> BokadeBöcker;
        public DateTime UtTid { get; set; }  //När man först planerar att hämta boken ifrån - 10 har jag plus 5 dagar att hämta boken
        public DateTime FaktisktUtTid { get; set; } //När boken faktiskt blev upphämtad av medlem DATETIME NOW
        public DateTime ÅterTid { get; set; } //När boken ska vara tillbaka lämnad, 14 dagar efter upphämtning
        public bool UppHämtad { get; set; }

        public Bokning(Expidit expidit, Medlem medlem, DateTime utTid, DateTime återTid, DateTime faktiskUtTid, IList<Bok> böcker, bool upphämtad)
        {
            _BokningsNr++;
            this.bokningsNr = _BokningsNr;
            Medlem = medlem;
            Expidit = expidit;
            BokadeBöcker = böcker;
            UtTid = utTid;
            FaktisktUtTid = faktiskUtTid;
            ÅterTid = återTid;
            UppHämtad = upphämtad;
        }
        public void Upphämtad()
        {
            UppHämtad = true;
        }
        public void InteUppHämtad()
        {
            UppHämtad = false;
        }

    }
}
