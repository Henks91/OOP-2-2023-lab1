using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    public class Medlem
    {
        public int MedlemsNr { get; set; }
        public string Namn { get; set; }
        public string TelefonNr { get; set; }
        public string Epost { get; set; }
        public Bokning bokad { get; set; }
        public Medlem(int medlemsNr, string namn, string telefonNr, string epost)
        {
            MedlemsNr = medlemsNr;
            Namn = namn;
            TelefonNr = telefonNr;
            Epost = epost;
        }
    }
}