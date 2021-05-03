using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllRoles
{
    public class GetAllRolesUseCase :
        UseCaseEndPointNodeBase<IGetAllRolesRequestContract, IGetAllRolesSuccessResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllRolesUseCase(ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<IGetAllRolesSuccessResultContract>> Ask(
            IGetAllRolesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.WorkerRoles
                .Select(a =>
                    new RoleViewModel()
                    {
                        Id = a.Id,
                        Name = a.Name
                    })
                .ToListAsync(cancellationToken: cancellationToken);
            return 
                new UseCaseResponse<IGetAllRolesSuccessResultContract>(
                    new GetAllRolesSuccessResultContract() {Data = result});
        }
    }
}