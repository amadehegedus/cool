using System;
using System.Collections.Generic;
using System.Text;
using Cool.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cool.Dal.Configurations
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");

            builder.HasOne(d => d.Caff)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.CaffId)
                .HasConstraintName("FK_Comment_Caff");
        }
    }
}
