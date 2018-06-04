using DogsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DogsWebApp.DAL
{
    public class DogsContext : DbContext
    {
        public DogsContext() : base("DogsCon")
        {
        }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<BreedType> BreedTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Overrides the table created to be named with a singular name
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}