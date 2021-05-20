using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllRolesByUser
{
    public class GetAllRolesByUser : EndPointNodeBase<IGetAllRolesByUserRequestContract, IGetAllRolesByUserResultContract>
    {
        private readonly ApplicationContext _context;
        public GetAllRolesByUser(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetAllRolesByUserResultContract>> Ask(
            IGetAllRolesByUserRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .WorkerRoles
                .Where(e => e.WorkerId == input.UserId)
                .Select(e => e.RoleId)
                .ToListAsync(cancellationToken: cancellationToken);
            return new UseCaseResponse<IGetAllRolesByUserResultContract>(new GetAllRolesByUserResultContract() { Roles = result });
        }
    }
}