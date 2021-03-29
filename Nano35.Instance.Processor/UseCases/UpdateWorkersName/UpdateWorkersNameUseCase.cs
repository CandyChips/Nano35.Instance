using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersName
{
    public class UpdateWorkersNameUseCase :
        EndPointNodeBase<
            IUpdateWorkersNameRequestContract,
            IUpdateWorkersNameResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateWorkersNameUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateWorkersNameSuccessResultContract : 
            IUpdateWorkersNameSuccessResultContract
        {
            
        }

        public override async Task<IUpdateWorkersNameResultContract> Ask(
            IUpdateWorkersNameRequestContract input,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}