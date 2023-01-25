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

        public bool Status { get; set; }
       

        public Bok(string titel, int isbn, bool status)
        {
            Titel = titel;
            ISBN = isbn;
            Status = status;
        }

        public void Tillgänglig()
        {
            Status = true;
        }
        public void Bokad()
        {
            Status = false;
        }

    }
}