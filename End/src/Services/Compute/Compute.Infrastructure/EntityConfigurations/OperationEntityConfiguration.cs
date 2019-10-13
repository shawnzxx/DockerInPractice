using Compute.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using static Compute.Domain.Models.Operation;

namespace Compute.Infrastructure.EntityConfigurations
{
    //https://jerry153fish.github.io/2018/02/16/DotNet-Study-Note-2-EF.html
    public class OperationEntityConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.ToTable("operations");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                   .UseHiLo(name: "OperationSeq")
                   .ValueGeneratedOnAdd();

            builder.Property(o => o.X)
                .IsRequired();
            builder.Property(o => o.Y)
                .IsRequired();
            builder.Property(o => o.Result)
                .IsRequired();

            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions
            builder.Property(o => o.OperationType)
                .IsRequired()
                .HasColumnName("OperationTypeStr")
                .HasMaxLength(50)
                .HasConversion(
                    enumValue => enumValue.ToString(),
                    stringValue => (OperationTypeEnum)Enum.Parse(typeof(OperationTypeEnum), stringValue)
                );
        }
    }
}
