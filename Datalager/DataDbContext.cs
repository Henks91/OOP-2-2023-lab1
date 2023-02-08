using Entiteter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datalager
{
    public class DataDbContext : DbContext
    {
        public DbSet<Bok> BokRepository
        {
            get; private set;
        }

        public DbSet<Expidit> ExpiditRepository
        {
            get; private set;
        }

        public DbSet<Bokning> BokningRepository
        {
            get; private set;
        }

        public DbSet<Faktura> FakturaRepository
        {
            get; private set;
        }

        public DbSet<Medlem> MedlemRepository
        {
            get; private set;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = sqlutb2.hb.se, 56077; Database = osu2324; User Id = osu2324; Password = xy9654; ");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
