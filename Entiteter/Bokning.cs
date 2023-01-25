using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    public class Bokning
    {
        public int BokningsNr { get; set; } = - 1;

        public Expidit Expidit;

        public Medlem Medlem;

        public IList<Bok> BokadeBöcker;
        public DateTime UtTid { get; set; }
        public DateTime FaktisktUtTid { get; set; } 
        public DateTime ÅterTid { get; set; }

        public Bokning(int bokningsNr, Expidit expidit, Medlem medlem, DateTime utTid, DateTime återTid, DateTime faktiskUtTid, IList<Bok> böcker)
        {
            BokningsNr = bokningsNr;
            Medlem = medlem;
            Expidit = expidit;
            BokadeBöcker = böcker;
            UtTid = utTid;
            FaktisktUtTid = faktiskUtTid;
            ÅterTid = återTid;
        }
       
    }
}
