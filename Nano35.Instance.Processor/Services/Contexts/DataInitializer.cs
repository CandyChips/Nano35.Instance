using System.Linq;
using System.Threading.Tasks;
using Nano35.Instance.Processor.Models;

namespace Nano35.Instance.Processor.Services.Contexts
{
    public class DataInitializer
    {
        public static async Task InitializeRolesAsync(
            ApplicationContext modelBuilder)
        {
            if(!modelBuilder.UnitTypes.Any())
            {
                await modelBuilder.ClientTypes.AddAsync(new ClientType() { Name = "Поставщик"});
                await modelBuilder.ClientTypes.AddAsync(new ClientType(){ Name = "Заказчик" });
                await modelBuilder.ClientTypes.AddAsync(new ClientType(){ Name = "Покупатель" });
                await modelBuilder.ClientTypes.AddAsync(new ClientType(){ Name = "Торгующая организация" });
                await modelBuilder.ClientStates.AddAsync(new ClientState(){ Name = "Юридическое лицо" });
                await modelBuilder.ClientStates.AddAsync(new ClientState(){ Name = "Физическое лицо" });
                await modelBuilder.InstanceTypes.AddAsync(new InstanceType(){ Name = "Сервисный центр" });
                await modelBuilder.UnitTypes.AddAsync(new UnitType(){ Name = "Склад" });
                await modelBuilder.UnitTypes.AddAsync(new UnitType(){ Name = "Сервис" });
                await modelBuilder.Regions.AddAsync(new Region(){ Name = "RUS" });
                await modelBuilder.Regions.AddAsync(new Region(){ Name = "UK" });
                await modelBuilder.Regions.AddAsync(new Region(){ Name = "EU" });
                await modelBuilder.WorkerRoles.AddAsync(new WorkersRole() { Name = "Администратор" });
                await modelBuilder.WorkerRoles.AddAsync(new WorkersRole() { Name = "Сотрудник" });
                
                await modelBuilder
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}