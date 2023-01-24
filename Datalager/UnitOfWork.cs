using System;
using System.Collections.Generic;
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

        }

        /// <summary>
        ///  Save the changes made. Does nothing in this case.
        /// </summary>
        public void Save() // Tillagd enligt Anders instruktion för att simulera hur det skulle vara om vi sparar
            { 

            }
            private void Fill()
            {
            #region Expiditer
                ExpiditRepository.Add(new Expidit(11, "Björn", "Björn1337", "Lärling"));

                ExpiditRepository.Add(new Expidit(22, "a", "a", "Chef"));
            #endregion Expediter

            #region Böcker

            BokRepository.Add(new Bok("Henriks mardrömshål", 1281894, true));
            BokRepository.Add(new Bok("Fredrik osynliga hjälten", 183943, true ));
            BokRepository.Add(new Bok("Carl & Den heliga Snusmanualen", 3435342, false));
            #endregion Böcker

            #region Medlemmar
            MedlemRepository.Add(new Medlem(1, "Lukas", "073-423532", "Lukas@lukasserver.se"));
            MedlemRepository.Add(new Medlem(2, "GandalfWhite", "073-13371337", "Gandalf@CarrierOfLight.se"));
            MedlemRepository.Add(new Medlem(3, "GandalfGrey", "073-73317331", "Gandalf@CarrierOfRock.se"));
            #endregion Medlemmar
            }
    }
}
