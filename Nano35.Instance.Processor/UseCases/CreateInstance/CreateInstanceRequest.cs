using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateInstance
{
    public class CreateInstanceRequest :
        EndPointNodeBase<
            ICreateInstanceRequestContract, 
            ICreateInstanceResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateInstanceRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateInstanceSuccessResultContract : 
            ICreateInstanceSuccessResultContract
        {
            
        }
        
        public override async Task<ICreateInstanceResultContract> Ask(
            ICreateInstanceRequestContract input,
            CancellationToken cancellationToken)
        {
            var role = this._context.WorkerRoles.FirstOrDefault();
            var instance = new Models.Instance(){
                Id = input.NewId,
                OrgEmail = input.Email,
                OrgName = input.Name,
                OrgRealName = input.RealName,
                CompanyInfo = input.Info,
                InstanceTypeId = input.TypeId,
                RegionId = input.RegionId
            };
            await _context.AddAsync(instance, cancellationToken);
            var defaultUser = new Worker(){
                Id = input.UserId,
                Instance = instance,
                WorkersRole = role,
                Name = "Администратор системы",
                Comment = ""
            };
            await _context.AddAsync(defaultUser, cancellationToken);
            return new CreateInstanceSuccessResultContract();
        }
    }
}