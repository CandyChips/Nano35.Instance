using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests.CreateCashInput
{
    public class CreateInputCashOperationRequest :
        IPipelineNode<
            ICreateCashInputRequestContract,
            ICreateCashInputResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateInputCashOperationRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateCashInputSuccessResultContract : 
            ICreateCashInputSuccessResultContract
        {
            
        }
        
        public async Task<ICreateCashInputResultContract> Ask(
            ICreateCashInputRequestContract input,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}