using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    class Bok
    {
        public string Titel { get; set; }
        public int ISBN { get; set; }
        public Bok(string titel, int isbn)
        {
            Titel = titel;
            ISBN = isbn;
        }
    }
}