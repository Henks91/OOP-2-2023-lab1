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

        public List<Bok> BokadeBöcker;
        public DateTime UtTid { get; set; }
        public DateTime FaktisktUtTid { get; set; }
        public DateTime ÅterTid { get; set; }

        public Bokning(int bokningsNr, Expidit expidit, Medlem medlem, DateTime utTid, DateTime återTid, DateTime faktiskUtTid)
        {
            BokningsNr = bokningsNr;
            Medlem = medlem;
            Expidit = expidit;
            BokadeBöcker = new List<Bok>();
            UtTid = utTid;
            FaktisktUtTid = faktiskUtTid;
            ÅterTid = återTid;
        }
       
    }
}
