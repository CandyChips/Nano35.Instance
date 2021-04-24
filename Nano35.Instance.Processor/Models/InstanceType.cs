using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class InstanceType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public class Configuration : IEntityTypeConfiguration<InstanceType>
        {
            public void Configure(EntityTypeBuilder<InstanceType> builder)
            {
                builder.ToTable("InstanceTypes");
                builder.HasKey(u => u.Id);        
                builder.Property(b => b.Name)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
            }
        }
    }
}