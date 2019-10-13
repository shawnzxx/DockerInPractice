using Audit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.Infrastructure.EntityConfigurations
{
    public class RunningToolEntityConfiguration : IEntityTypeConfiguration<RunningTotal>
    {
        public void Configure(EntityTypeBuilder<RunningTotal> builder)
        {
            builder.ToTable("runningtotals");
            builder.HasKey(r => r.Id);
            builder.Property(o => o.Id)
                   .UseHiLo(name: "runningtotalseq")
                   .ValueGeneratedOnAdd();

            //we can't set Property name in domain level
            //this is because of proerty name RunningTotal same as class name
            //but we still want column name as RunningTotal
            builder.Property(r => r.Total).HasColumnName("RunningTotal").IsRequired();
            builder.Property(r => r.UpdatedDate).IsRequired();

        }
    }
}
