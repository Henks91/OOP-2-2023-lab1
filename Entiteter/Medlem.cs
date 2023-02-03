namespace Entiteter
{
    public class Medlem
    {
        public int MedlemsNr { get; private set; }
        public string Namn { get; private set; } //hade varit public vid verkligt exempel då folk kan byta namn
        public string TelefonNr { get; private set; } //här gäller vi samma som ovan gällande telefonnummer
        public string Epost { get; private set; } // se ovan kommentarer

        public Medlem(int medlemsNr, string namn, string telefonNr, string epost)
        {
            MedlemsNr = medlemsNr;
            Namn = namn;
            TelefonNr = telefonNr;
            Epost = epost;
        }
    }
}