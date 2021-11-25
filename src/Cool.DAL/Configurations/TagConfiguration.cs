using System;
using System.Collections.Generic;
using System.Text;
using Cool.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cool.Dal.Configurations
{
    internal class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");

            builder.HasOne(d => d.Caff)
                .WithMany(p => p.Tags)
                .HasForeignKey(d => d.CaffId)
                .HasConstraintName("FK_Tag_Caff");
        }
    }
}
