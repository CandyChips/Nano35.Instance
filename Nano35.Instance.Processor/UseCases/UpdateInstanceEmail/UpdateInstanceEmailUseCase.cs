using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceEmail
{
    public class UpdateInstanceEmailUseCase :
        EndPointNodeBase<
            IUpdateInstanceEmailRequestContract, 
            IUpdateInstanceEmailResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateInstanceEmailUseCase(ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<IUpdateInstanceEmailResultContract> Ask(
            IUpdateInstanceEmailRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Instances.FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken);
            result.OrgEmail = input.Email;
            return new UpdateInstanceEmailSuccessResultContract();
        }
    }
}