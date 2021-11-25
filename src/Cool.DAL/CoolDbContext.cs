using Cool.Dal.Configurations;
using Cool.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cool.Dal
{
    public class CoolDbContext : DbContext
    {
        public DbSet<Caff> Caffs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

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
