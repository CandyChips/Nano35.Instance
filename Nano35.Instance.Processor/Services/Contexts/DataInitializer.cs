using System;
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
                await modelBuilder.ClientTypes.AddAsync(new ClientType() { Name = "Поставщик", Id = Guid.Parse("B1B64C17-588B-4C7B-97E7-08D90BCF665E")});
                await modelBuilder.ClientTypes.AddAsync(new ClientType() { Name = "Заказчик", Id = Guid.Parse("249C94BB-20D6-4941-97E5-08D90BCF665E")});
                await modelBuilder.ClientTypes.AddAsync(new ClientType() { Name = "Покупатель", Id = Guid.Parse("A4579665-4592-454E-97E6-08D90BCF665E")});
                await modelBuilder.ClientTypes.AddAsync(new ClientType() { Name = "Торгующая организация" });
                await modelBuilder.ClientStates.AddAsync(new ClientState() { Name = "Юридическое лицо", Id = Guid.Parse("0A0E079D-DD41-4009-EB9C-08D90BCF6667")});
                await modelBuilder.ClientStates.AddAsync(new ClientState() { Name = "Физическое лицо", Id = Guid.Parse("9F76E798-AAB2-44CF-EB9B-08D90BCF6667")});
                await modelBuilder.InstanceTypes.AddAsync(new InstanceType() { Name = "Сервисный центр", Id = Guid.Parse("C2AA753C-458D-4390-FFDD-08D90BCF6668")});
                await modelBuilder.UnitTypes.AddAsync(new UnitType() { Id = Guid.Parse("1A171F1F-ADE6-4414-8309-8B7306BDE8AA"), Name = "Склад" });
                await modelBuilder.UnitTypes.AddAsync(new UnitType() { Id = Guid.Parse("64817093-1363-41CD-0E66-08D90BCF666A"), Name = "Сервис" });
                await modelBuilder.Regions.AddAsync(new Region() { Name = "RUS", Id = Guid.Parse("662E4CB3-A680-47F4-1C59-08D90BCF666B")});
                await modelBuilder.Regions.AddAsync(new Region() { Name = "UK" });
                await modelBuilder.Regions.AddAsync(new Region() { Name = "EU" });
                await modelBuilder.Roles.AddAsync(new Role() { Id = Guid.Parse("45050AE0-06C8-4D5C-AE5C-4E6831504CA3"), Name = "Менеджер" });
                await modelBuilder.Roles.AddAsync(new Role() { Id = Guid.Parse("0474CDCA-29F0-43F4-B365-3ACCDDFC1A27"), Name = "Кладовщик" });
                await modelBuilder.Roles.AddAsync(new Role() { Id = Guid.Parse("BDEE02B1-1B7A-4AAB-A269-7A9235287AF9"), Name = "Мастер" });
                await modelBuilder.Roles.AddAsync(new Role() { Id = Guid.Parse("35075B7C-74B6-415D-9D16-5FB089C5B4BE"), Name = "Бухгалтер" });
                await modelBuilder.Roles.AddAsync(new Role() { Id = Guid.Parse("58688AB3-A30C-4385-9310-F20FEA1D4082"), Name = "Администратор" });
                await modelBuilder.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}