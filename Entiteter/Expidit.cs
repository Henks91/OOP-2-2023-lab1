namespace Entiteter
{
    public class Expidit
    {
        public int AnstNr { get; private set; }
        public string Namn { get; private set; }
        private string lösenord; // hade varit en property ifall vi haft ett "riktigt" program vi arbetat med...
        public string Roll { get; private set; }
        public Expidit(int anstNr, string namn, string lösenord, string roll)
        {
            AnstNr = anstNr;
            Namn = namn;
            this.lösenord = lösenord;
            Roll = roll;
        }
        public bool Lösenordskontroll(string försök)
        {
            return lösenord == försök;
        }
    }
}