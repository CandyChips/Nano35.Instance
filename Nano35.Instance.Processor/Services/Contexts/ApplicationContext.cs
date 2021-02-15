using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Instance.Processor.Models;

namespace Nano35.Instance.Processor.Services.Contexts
{
    public class ApplicationContext : DbContext
    {
        public enum CashOperationTypes
        {
            Input = 1,
            Output = 2,
            Coming = 3,
            Selle = 4
        }
        public DbSet<Models.Instance> Instances { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<LicenseType> LicenseTypes { get; set; }
        public DbSet<Region> Regions {get;set;}
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<CashOperation> CashOperations { get; set; }
        public DbSet<WorkersRole> WorkerRoles { get; set; }
        public DbSet<InstanceType> InstanceTypes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientState> ClientStates { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        
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
            FluentContext.Unit(modelBuilder);
            FluentContext.UnitType(modelBuilder);
            FluentContext.Worker(modelBuilder);
            FluentContext.WorkersRole(modelBuilder);
            FluentContext.CashOperation(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        public void Update()
        {
            CashOperations.Load();
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
