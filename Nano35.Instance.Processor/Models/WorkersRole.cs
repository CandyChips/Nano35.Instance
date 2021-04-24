using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts.Instance.Models;

namespace Nano35.Instance.Processor.Models
{
    public class WorkersRole
    {
        //Primary key
        public Guid Id { get; set; }
        //Data
        public string Name { get; set; }
        
        public class Configuration : IEntityTypeConfiguration<WorkersRole>
        {
            public void Configure(EntityTypeBuilder<WorkersRole> builder)
            {
                builder.ToTable("WorkersRoles");
                builder.HasKey(u => u.Id);
                builder.Property(b => b.Name)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
            }
        }
    }
}