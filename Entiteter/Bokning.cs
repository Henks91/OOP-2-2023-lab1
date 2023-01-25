using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    public class Bokning
    {       
        private static int _BokningsNr = 0;
        private int bokningsNr = 0;
        public int BokningsNr { get { return bokningsNr; } }

        public Expidit Expidit { get; private set; } //fundera ´kring public här

        public Medlem Medlem { get; private set; }

        public IList<Bok> BokadeBöcker;
        public DateTime UtTid { get; private set; }
        public DateTime FaktisktUtTid { get; private set; } 
        public DateTime ÅterTid { get; private set; }

        public Bokning(Expidit expidit, Medlem medlem, DateTime utTid, DateTime återTid, DateTime faktiskUtTid, IList<Bok> böcker)
        {
            _BokningsNr ++;
            this.bokningsNr = _BokningsNr;
            Medlem = medlem;
            Expidit = expidit;
            BokadeBöcker = böcker;
            UtTid = utTid;
            FaktisktUtTid = faktiskUtTid;
            ÅterTid = återTid;
        }
       
    }
}
