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

        public Expidit Expidit; //fundera ´kring public här

        public Medlem Medlem;

        public IList<Bok> BokadeBöcker;
        public DateTime UtTid { get; set; }
        public DateTime FaktisktUtTid { get; set; } 
        public DateTime ÅterTid { get; set; }

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
