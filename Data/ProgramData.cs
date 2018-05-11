using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class TravelAgentContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = (localdb)\\mssqllocaldb; Database = TravelAgency; Trusted_Connection = True; ");


        }

    }

    class ProgramData
    {
            static void Main(string[] args)
            {

            }
    }

}
