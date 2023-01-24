using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    class Bokning
    {
        public int BokningsNr = 1;

        public Expidit Expidit;

        public Medlem Medlem;

        public List<Bok> BokadeBöcker;
        public DateTime UtTid { get; set; }
        public DateTime FaltiskUtTid { get; set; }
        public DateTime ÅterTid { get; set; }

        public Bokning(int bokningsNr, Expidit expidit, Medlem medlem, Bok bokadeBöcker, DateTime utTid, DateTime faktiskUtTid, DateTime återTid)
        {
            BokningsNr = bokningsNr++;
            Expidit = expidit;
            Medlem = medlem;
            BokadeBöcker = new List<Bok>();
            UtTid = utTid;
            FaltiskUtTid = faktiskUtTid;
            ÅterTid = återTid;
        }
    }
}
