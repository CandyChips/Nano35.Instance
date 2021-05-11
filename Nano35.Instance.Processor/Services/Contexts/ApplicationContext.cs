using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Instance.Processor.Models;

namespace Nano35.Instance.Processor.Services.Contexts
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Models.Instance> Instances { get; set; }
        public DbSet<Region> Regions {get;set;}
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<WorkersRole> WorkerRoles { get; set; }
        public DbSet<InstanceType> InstanceTypes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientState> ClientStates { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            Update();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientProfile.Configuration());
            modelBuilder.ApplyConfiguration(new Client.Configuration());
            modelBuilder.ApplyConfiguration(new ClientState.Configuration());
            modelBuilder.ApplyConfiguration(new ClientType.Configuration());
            modelBuilder.ApplyConfiguration(new Models.Instance.Configuration());
            modelBuilder.ApplyConfiguration(new InstanceType.Configuration());
            modelBuilder.ApplyConfiguration(new Unit.Configuration());
            modelBuilder.ApplyConfiguration(new UnitType.Configuration());
            modelBuilder.ApplyConfiguration(new Worker.Configuration());
            modelBuilder.ApplyConfiguration(new WorkersRole.Configuration());
            modelBuilder.ApplyConfiguration(new Role.Configuration());
            base.OnModelCreating(modelBuilder);
        }

        private void Update()
        {
            Regions.Load();
            Units.Load();
            UnitTypes.Load();
            Workers.Load();
            WorkerRoles.Load();
            Instances.Load();
            InstanceTypes.Load();
            ClientProfiles.Load();
            Clients.Load();
            ClientStates.Load();
            ClientTypes.Load();
            Roles.Load();
        }
    }
}
