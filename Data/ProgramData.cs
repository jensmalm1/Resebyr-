using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class TravelAgencyContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = (localdb)\\mssqllocaldb; Database = TravelAgency; Trusted_Connection = True; ");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>()
                .HasKey(x => new { x.CustomerId, x.TravelId });
        }
    }


    class ProgramData
    {
            static void Main(string[] args)
            {

            }
    }

}
