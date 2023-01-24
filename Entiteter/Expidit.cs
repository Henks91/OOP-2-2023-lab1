namespace Entiteter
{
    public class Expidit
    {
        public int AnstNr { get; set; }
        public string Namn { get; set; }
        private string Lösenord { get; set; }
        public string Roll { get; set; }
        public Expidit(int anstNr, string namn, string lösenord, string roll)
        {
            AnstNr = anstNr;
            Namn = namn;
            Lösenord = lösenord;
            Roll = roll;
        }

        public bool Lösenordskontroll(string försök)
        {
            return Lösenord == försök;
        }

    }
}