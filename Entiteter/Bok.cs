using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    public class Bok
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
    }
}