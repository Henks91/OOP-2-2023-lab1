using Entiteter.Interface;

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