using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllCashOperations
{
    public class GetAllCashOperationsRequest :
        IPipelineNode<
            IGetAllCashOperationsRequestContract,
            IGetAllCashOperationsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllCashOperationsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllCashOperationsSuccessResultContract : 
            IGetAllCashOperationsSuccessResultContract
        {
            public IEnumerable<ICashOperationViewModel> Data { get; set; }
        }
        
        public async Task<IGetAllCashOperationsResultContract> Ask(
            IGetAllCashOperationsRequestContract input,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            var operation = new CashOperation()
            {
                Id = input.InstanceId,
                UnitId = input.UnitId,
                //From = input.From,
                //To = input.To,
                //OperationType = input.CashOperationType
              
            };
            await _context.CashOperations.AddAsync(operation, cancellationToken);
            return new GetAllCashOperationsSuccessResultContract();
        }
    }
}