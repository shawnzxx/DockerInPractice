using Compute.Domain.Models;
using Compute.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compute.Infrastructure
{
    public class OperationDbContext : DbContext
    {
        //we add OperationDbContext to the ASP.net Core IoC container with options, so that we can use it in Startup.cs
        public OperationDbContext(DbContextOptions<OperationDbContext> options) : base(options)
        {
            //Database.Migrate();
        }
        public DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OperationEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
