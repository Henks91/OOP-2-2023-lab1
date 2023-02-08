using System;
using System.Collections.Generic;
using System.Text;
using Entiteter;

namespace Datalager.Seed
{
    public class DataSeed
    {
        public void Populate(DataDbContext dataDbContext)
        {
            
            #region Expiditer
            dataDbContext.ExpiditRepository.Add(new Expidit(11, "Magnus", "a", "Utvecklingsansvarig"));
            dataDbContext.ExpiditRepository.Add(new Expidit(22, "Gustaf", "b", "Bibliotekarie"));
            dataDbContext.ExpiditRepository.Add(new Expidit(33, "Josefin", "c", "Platschef"));
            dataDbContext.ExpiditRepository.Add(new Expidit(44, "Magaret", "d", "Bibliotekarie"));
            #endregion Expediter

            #region Böcker

            dataDbContext.BokRepository.Add(new Bok("Bröd Och Mjölk", 1281894, true));
            dataDbContext.BokRepository.Add(new Bok("Allt är mitt", 183943, true));
            dataDbContext.BokRepository.Add(new Bok("Under Magnoliaträden", 3435342, true));
            dataDbContext.BokRepository.Add(new Bok("Den andre", 442312, true));
            dataDbContext.BokRepository.Add(new Bok("Icebreaker", 342345, true));
            dataDbContext.BokRepository.Add(new Bok("Harry Potter", 656431, true));
            dataDbContext.BokRepository.Add(new Bok("Det slutar med oss", 333124, true));
            dataDbContext.BokRepository.Add(new Bok("Löpa Varg", 978554, true));
            dataDbContext.BokRepository.Add(new Bok("Blodmåne", 1023451, true));
            #endregion Böcker

            #region Medlemmar
            dataDbContext.MedlemRepository.Add(new Medlem(001, "Lukas Blomström", "073-4235322", "Lukas.blomström@hotmail.com"));
            dataDbContext.MedlemRepository.Add(new Medlem(002, "Jack Stenman", "073-13371337", "jacksten@gmail.com"));
            dataDbContext.MedlemRepository.Add(new Medlem(003, "Linn Aschberg", "073-73317331", "linn-aschberg@live.se"));
            #endregion Medlemmar

            dataDbContext.SaveChanges();
        }

    }
}

