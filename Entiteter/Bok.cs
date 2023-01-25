using Entiteter.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    public class Bok : IBok
    {
        public string Titel { get; set; }
        public int ISBN { get; set; }

        public bool ÄrTillgänglig { get; set; }

       

        public Bok(string titel, int isbn, bool status)
        {
            Titel = titel;
            ISBN = isbn;
            ÄrTillgänglig = status;
        }

        public void Tillgänglig()
        {
            ÄrTillgänglig = true;
        }
        public void Bokad()
        {
            ÄrTillgänglig = false;
        }
       
    }
}