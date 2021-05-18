using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class ClientTypes
    {
        public static Guid RepairOrderClient => Guid.Parse("249c94bb-20d6-4941-97e5-08d90bcf665e");    
        public static Guid SaleClient => Guid.Parse("a4579665-4592-454e-97e6-08d90bcf665e");    
        public static Guid ComingClient => Guid.Parse("b1b64c17-588b-4c7b-97e7-08d90bcf665e");    
        public static Guid SaleOrgClient => Guid.Parse("7db57451-8e3b-428f-2f4b-08d914af9ae9");    
    }
    
    public class ClientType
    {
        public Guid Id { get; set; }
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
