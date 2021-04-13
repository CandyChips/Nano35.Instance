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
        EndPointNodeBase<IGetAllRolesRequestContract, IGetAllRolesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllRolesUseCase(ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IGetAllRolesResultContract> Ask(
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
            return new GetAllRolesSuccessResultContract() {Data = result};
        }
    }
}