using MWD.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWD.ArvixeSQL
{
    public partial class ArvixeDB : DbContext
    {
        public ArvixeDB()
            : base("name=ArvixeDB")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("mwd");
            modelBuilder.Entity<NewEntityPrime>()
                .HasMany(c => c.Subs)
                .WithRequired()
                .HasForeignKey(s => s.ForeignKey_ID);
            modelBuilder.Entity<NewEntityPrimeAlternate>()
                .HasMany(c => c.Subs)
                .WithRequired()
                .HasForeignKey(s => s.ForeignKey_ID);

        }

        public DbSet<Person> People { get; set; }
        public DbSet<Email> EmailAddresses { get; set; }
        public DbSet<NewEntitySub> Subs { get; set; }
        public DbSet<NewEntityPrime> Primes { get; set; }
        public DbSet<NewEntityPrimeAlternate> Alts { get; set; }
    }
}
