using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class ClientType
    {
        //Primary key
        public Guid Id { get; set; }
        //Data
        public string Name { get; set; }
        public ICollection<Client> Clients { get; set; }

        public ClientType()
        {
            Clients = new List<Client>();
        }
        
        public class Configuration : IEntityTypeConfiguration<ClientType>
        {
            public void Configure(EntityTypeBuilder<ClientType> builder)
            {
                builder.ToTable("ClientTypes");
                builder.HasKey(u => u.Id);        
                builder.Property(b => b.Name)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
            }
        }
    }
}
