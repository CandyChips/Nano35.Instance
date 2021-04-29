using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstancePhone
{
    public class UpdateInstancePhoneUseCase :
        EndPointNodeBase<
            IUpdateInstancePhoneRequestContract, 
            IUpdateInstancePhoneResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateInstancePhoneUseCase(ApplicationContext context) => _context = context;
        public override async Task<IUpdateInstancePhoneResultContract> Ask(
            IUpdateInstancePhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfInstance = await _context.Instances.FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken);
            return new UpdateInstancePhoneSuccessResultContract() ;
        }
    }
}