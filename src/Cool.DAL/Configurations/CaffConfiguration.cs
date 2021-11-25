using System;
using System.Collections.Generic;
using System.Text;
using Cool.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cool.Dal.Configurations
{
    internal class CaffConfiguration : IEntityTypeConfiguration<Caff>
    {
        public void Configure(EntityTypeBuilder<Caff> builder)
        {
            builder.ToTable("Caff");
        }
    }
}
