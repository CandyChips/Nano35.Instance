using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests.CreateCashOutput
{
    public class CreateOutputCashOperationRequest :
        IPipelineNode<
            ICreateCashOutputRequestContract,
            ICreateCashOutputResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateOutputCashOperationRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateCashOutputSuccessResultContract : 
            ICreateCashOutputSuccessResultContract
        {
            
        }
        
        public async Task<ICreateCashOutputResultContract> Ask(
            ICreateCashOutputRequestContract input,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}