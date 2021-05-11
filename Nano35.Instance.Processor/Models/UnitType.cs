using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class UnitType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Unit> Units { get; set; }
        public UnitType() => Units = new List<Unit>();

        public class Configuration : IEntityTypeConfiguration<UnitType>
        {
            public void Configure(EntityTypeBuilder<UnitType> builder)
            {
                builder.ToTable("UnitTypes");
                builder.HasKey(u => u.Id);
                builder.Property(b => b.Name)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
            }
        }
    }
}
