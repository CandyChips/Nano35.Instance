using System;
using Microsoft.EntityFrameworkCore;

namespace Nano35.Instance.Processor.Models
{
    public class ClientState
    {
        //Primary key
        public Guid Id { get; set; }
        //Data
        public string Name { get; set; }
    }

    public partial class FluentContext 
    {
        public static void ClientState(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<ClientState>()
                .HasKey(u => u.Id);        
            //Data
            modelBuilder.Entity<ClientState>()
                .Property(b => b.Name)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
        }
    }
}
