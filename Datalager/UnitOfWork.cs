using Entiteter;

namespace Datalager
{
    /// <summary>
    ///  This class is used to access the storage in the application.
    /// </summary>
    public class UnitOfWork
    {
        public Repository<Bok> BokRepository
        {
            get; private set;
        }

        public Repository<Expidit> ExpiditRepository
        {
            get; private set;
        }

        public Repository<Bokning> BokningRepository
        {
            get; private set;
        }

        public Repository<Faktura> FakturaRepository
        {
            get; private set;
        }

        public Repository<Medlem> MedlemRepository
        {
            get; private set;
        }

        /// <summary>
        ///  Create a new instance.
        /// </summary>
        public UnitOfWork()
        {

            BokRepository = new Repository<Bok>();
            ExpiditRepository = new Repository<Expidit>();
            BokningRepository = new Repository<Bokning>();
            FakturaRepository = new Repository<Faktura>();
            MedlemRepository = new Repository<Medlem>();

            // Initialize the tables if this is the first UnitOfWork.
            if (ExpiditRepository.IsEmpty())
            {
                Fill();
            }
        }

        /// <summary>
        ///  Save the changes made. Does nothing in this case.
        /// </summary>
        public void Save() // Tillagd enligt Anders instruktion för att simulera hur det skulle vara om vi sparar
        {

        }

        private void Fill()  //hårdkodad data
        {
            #region Expiditer
            ExpiditRepository.Add(new Expidit(11, "Magnus", "a", "Utvecklingsansvarig"));
            ExpiditRepository.Add(new Expidit(22, "Gustaf", "b", "Bibliotekarie"));
            ExpiditRepository.Add(new Expidit(33, "Josefin", "c", "Platschef"));
            ExpiditRepository.Add(new Expidit(44, "Magaret", "d", "Bibliotekarie"));
            #endregion Expediter

            #region Böcker

            BokRepository.Add(new Bok("Bröd Och Mjölk", 1281894, true));
            BokRepository.Add(new Bok("Allt är mitt", 183943, true));
            BokRepository.Add(new Bok("Under Magnoliaträden", 3435342, true));
            BokRepository.Add(new Bok("Den andre", 442312, true));
            BokRepository.Add(new Bok("Icebreaker", 342345, true));
            BokRepository.Add(new Bok("Harry Potter", 656431, true));
            BokRepository.Add(new Bok("Det slutar med oss", 333124, true));
            BokRepository.Add(new Bok("Löpa Varg", 978554, true));
            BokRepository.Add(new Bok("Blodmåne", 1023451, true));
            #endregion Böcker

            #region Medlemmar
            MedlemRepository.Add(new Medlem(001, "Lukas Blomström", "073-4235322", "Lukas.blomström@hotmail.com"));
            MedlemRepository.Add(new Medlem(002, "Jack Stenman", "073-13371337", "jacksten@gmail.com"));
            MedlemRepository.Add(new Medlem(003, "Linn Aschberg", "073-73317331", "linn-aschberg@live.se"));
            #endregion Medlemmar

            #region Bokningar

            //BokningRepository.Add
            //   (new Bokning(99, ExpiditRepository.FirstOrDefault(e => e.AnstNr == 11 || e.AnstNr == 22),MedlemRepository.FirstOrDefault(m => m.MedlemsNr == 2),
            //                    BokRepository.FirstOrDefault(b => b.ISBN == 3435342),
            //                    DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), DateTime.Now + TimeSpan.FromDays(14)));

            //MedlemRepository.FirstOrDefault(m => m.MedlemsNr == 2).bokad =
            //    BokningRepository.FirstOrDefault(b => true);

            #endregion
        }
    }
}
