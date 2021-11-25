using Microsoft.EntityFrameworkCore;

namespace Cool.Dal.Configurations
{
    public class EntityConfigurations
    {
        public static void ConfigureAllEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CaffConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
        }
    }
}
