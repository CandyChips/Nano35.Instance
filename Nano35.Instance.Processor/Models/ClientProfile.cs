using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class ClientProfile
    {
        public Guid Id { get; set; }
        public string Phone { get; set; }
        public ICollection<Client> Clients { get; set; }

        public ClientProfile()
        {
            Clients = new List<Client>();
        }
        
        public class Configuration : IEntityTypeConfiguration<ClientProfile>
        {
            public void Configure(EntityTypeBuilder<ClientProfile> builder)
            {
                builder.ToTable("ClientProfiles");
                builder.HasKey(u => u.Id);
                builder.Property(b => b.Phone).HasColumnType("nvarchar(MAX)").IsRequired();
            }
        }
    }
}