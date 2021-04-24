using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class ClientState
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public class Configuration : IEntityTypeConfiguration<ClientState>
        {
            public void Configure(EntityTypeBuilder<ClientState> builder)
            {
                builder.ToTable("ClientStates");
                builder.HasKey(u => u.Id);        
                builder.Property(b => b.Name)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
            }
        }
    }
}
