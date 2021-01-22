using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Instance.Processor.Models;

namespace Nano35.Instance.Processor.Services.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Models.Instance> Instances { get; set; }
        public DbSet<Models.License> Licenses { get; set; }
        public DbSet<Models.LicenseType> LicenseTypes { get; set; }
        public DbSet<Models.Region> Regions {get;set;}
        public DbSet<Models.Unit> Units { get; set; }
        public DbSet<Models.UnitType> UnitTypes { get; set; }
        public DbSet<Models.Worker> Workers { get; set; }
        public DbSet<Models.WorkersRole> WorkerRoles { get; set; }
        public DbSet<Models.InstanceType> InstanceTypes { get; set; }
        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Models.ClientState> ClientStates { get; set; }
        public DbSet<Models.ClientType> ClientTypes { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
            Update();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            FluentContext.Client(modelBuilder);
            FluentContext.ClientState(modelBuilder);
            FluentContext.ClientType(modelBuilder);
            FluentContext.Instance(modelBuilder);
            FluentContext.InstanceType(modelBuilder);
            FluentContext.License(modelBuilder);
            FluentContext.LicenseType(modelBuilder);
            FluentContext.SalleType(modelBuilder);
            FluentContext.Unit(modelBuilder);
            FluentContext.UnitType(modelBuilder);
            FluentContext.Worker(modelBuilder);
            FluentContext.WorkersRole(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        public void Update()
        {
            Regions.Load();
            Units.Load();
            Licenses.Load();
            LicenseTypes.Load();
            Units.Load();
            UnitTypes.Load();
            Workers.Load();
            WorkerRoles.Load();
            InstanceTypes.Load();
            Clients.Load();
            ClientStates.Load();
            ClientTypes.Load();
        }
    }
    public class DataInitializer
    {
        public static async Task InitializeRolesAsync(
            ApplicationContext modelBuilder)
        {
            if(!modelBuilder.UnitTypes.Any())
            {
                modelBuilder.ClientTypes.Add(new Models.ClientType(){
                    Name = "Поставщик" 
                });
                modelBuilder.ClientTypes.Add(new Models.ClientType(){
                    Name = "Заказчик" 
                });
                modelBuilder.ClientTypes.Add(new Models.ClientType(){
                    Name = "Покупатель" 
                });
                modelBuilder.ClientTypes.Add(new Models.ClientType(){
                    Name = "Торгующая организация" 
                });
                modelBuilder.ClientStates.Add(new Models.ClientState(){
                    Name = "Юридическое лицо"
                });
                modelBuilder.ClientStates.Add(new Models.ClientState(){
                    Name = "Физическое лицо"
                });
                modelBuilder.InstanceTypes.Add(new Models.InstanceType(){
                    Name = "Сервисный центр" 
                });
                modelBuilder.UnitTypes.Add(new Models.UnitType(){
                    Name = "Склад" 
                });
                modelBuilder.UnitTypes.Add(new Models.UnitType(){
                    Name = "Сервис" 
                });
                modelBuilder.Regions.Add(new Models.Region(){
                    Name = "RUS",
                    CashType = "руб",
                    Rate = 1
                });
                modelBuilder.Regions.Add(new Models.Region(){
                    Name = "UK",
                    CashType = "гривн",
                    Rate = 1
                });
                modelBuilder.Regions.Add(new Models.Region(){
                    Name = "EU",
                    CashType = "евро",
                    Rate = 1
                });
                modelBuilder.LicenseTypes.Add(new Models.LicenseType(){
                    Name="Тест",
                    Descriprion = "Разработка",
                    Price = 0,
                    Date = DateTime.Now 
                });
                modelBuilder.WorkerRoles.Add(new WorkersRole() {
                    Name = "Администратор"
                });
                modelBuilder.WorkerRoles.Add(new WorkersRole() {
                    Name = "Сотрудник"
                });
                
                await modelBuilder
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}
