using Entiteter.Interface;

namespace Entiteter
{
    public class Bok : IBok
    {
        public string Titel { get; private set; }
        public int ISBN { get; private set; }
        public bool ÄrTillgänglig { get; set; }

        public Bok(string titel, int isbn, bool status)
        {
            Titel = titel;
            ISBN = isbn;
            ÄrTillgänglig = status;
        }
        public void Tillgänglig() //ändrar bokens bool värde vid återlämning av böcker
        {
            ÄrTillgänglig = true;
        }
        public void Bokad() //ändrar bokens bool värde vid skapandet av en bokning
        {
            ÄrTillgänglig = false;
        }

    }
}