using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    public class Bokning
    {
        public int BokningsNr { get; set; }

        public Expidit Expidit;

        public Medlem Medlem;

        public List<Bok> BokadeBöcker;
        public DateTime UtTid { get; set; }
        public DateTime FaktisktUtTid { get; set; }
        public DateTime ÅterTid { get; set; }

        public Bokning(int bokningsNr, Expidit expidit, Medlem medlem, DateTime faktiskUtTid, DateTime utTid, DateTime återTid)
        {
            BokningsNr = bokningsNr++;
            Expidit = expidit;
            Medlem = medlem;
            BokadeBöcker = new List<Bok>();
            FaktisktUtTid = faktiskUtTid;
            UtTid = utTid;          
            ÅterTid = återTid;
        }
       
    }
}
