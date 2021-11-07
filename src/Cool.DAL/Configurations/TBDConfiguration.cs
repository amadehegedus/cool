using System;
using System.Collections.Generic;
using System.Text;
using Cool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cool.DAL.Configurations
{
    internal class TBDConfiguration : IEntityTypeConfiguration<TBD>
    {
        public void Configure(EntityTypeBuilder<TBD> builder)
        {
            builder.ToTable("TBD");

            //FK példa:
            //builder.HasOne(d => d.Head)
            //    .WithMany(p => p.ContractPartners)
            //    .HasForeignKey(d => d.ContractHeadId)
            //    .HasConstraintName("FK_ContractPartner_ContractHead");
        }
    }
}
