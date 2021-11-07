using System;
using System.Collections.Generic;
using System.Text;
using Cool.DAL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cool.DAL
{
    public class CoolDbContext : DbContext
    {
        // public DbSet<TBD> TBDs { get; set; }

        public CoolDbContext(DbContextOptions<CoolDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityConfigurations.ConfigureAllEntities(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
