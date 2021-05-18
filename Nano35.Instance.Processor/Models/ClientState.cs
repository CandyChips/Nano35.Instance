using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class ClientStates
    {
        public static Guid Organisation => Guid.Parse("0a0e079d-dd41-4009-eb9c-08d90bcf6667");    
        public static Guid Person => Guid.Parse("9f76e798-aab2-44cf-eb9b-08d90bcf6667");    
    }

    public class ClientState
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Client> Clients { get; set; }

        public ClientState()
        {
            Clients = new List<Client>();
        }
        
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
