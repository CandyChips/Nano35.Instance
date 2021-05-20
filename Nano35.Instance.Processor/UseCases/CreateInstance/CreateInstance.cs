using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateInstance
{
    public class CreateInstance : EndPointNodeBase<ICreateInstanceRequestContract, ICreateInstanceResultContract>
    {
        private readonly ApplicationContext _context;
        public CreateInstance(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<ICreateInstanceResultContract>> Ask(
            ICreateInstanceRequestContract input,
            CancellationToken cancellationToken)
        {
            var role = _context.WorkerRoles.FirstOrDefault();
            if (!_context.Regions.Any(e => e.Id == input.RegionId))
                return Pass("Неверный регион.");
            if (!_context.InstanceTypes.Any(e => e.Id == input.TypeId))
                return Pass("Неверный тип организации.");
            if (_context.ClientProfiles.Any(e => e.Id == input.NewId))
                return Pass("Повторите попытку позже.");
            
            var instance = 
                new Models.Instance()
                    {Id = input.NewId,
                     OrgEmail = input.Email,
                     OrgName = input.Name,
                     OrgRealName = input.RealName,
                     CompanyInfo = input.Info,
                     InstanceTypeId = input.TypeId,
                     RegionId = input.RegionId};
            
            await _context.AddAsync(instance, cancellationToken);
            
            var defaultUser = 
                new Worker()
                    {Id = input.UserId,
                     Instance = instance,
                     Name = "Администратор системы",
                     Comment = ""};
            
            await _context.AddAsync(defaultUser, cancellationToken);

            var setsAdminRole = new WorkersRole()
            {
                Id = Guid.NewGuid(),
                RoleId = Roles.Admin,
                WorkerId = input.NewId
            };
            
            await _context.AddAsync(setsAdminRole, cancellationToken);
            
            return Pass(new CreateInstanceResultContract());
        }
    }
}