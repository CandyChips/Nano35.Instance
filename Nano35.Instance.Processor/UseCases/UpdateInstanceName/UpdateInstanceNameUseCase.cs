using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceName
{
    public class UpdateInstanceNameUseCase :
        EndPointNodeBase<
            IUpdateInstanceNameRequestContract,
            IUpdateInstanceNameResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateInstanceNameUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateInstanceNameSuccessResultContract : 
            IUpdateInstanceNameSuccessResultContract
        {
            
        }

        public override async Task<IUpdateInstanceNameResultContract> Ask(
            IUpdateInstanceNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Instances
                    .FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken)
                );

            result.OrgName = input.Name;
            return new UpdateInstanceNameSuccessResultContract();
        }
    }
}