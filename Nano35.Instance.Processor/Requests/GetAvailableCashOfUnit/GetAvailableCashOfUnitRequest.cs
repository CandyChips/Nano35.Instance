using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests.GetAvailableCashOfUnit
{
    public class GetAvailableCashOfUnitRequest :
        IPipelineNode<
            IGetAvailableCashOfUnitRequestContract,
            IGetAvailableCashOfUnitResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAvailableCashOfUnitRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAvailableCashOfUnitSuccessResultContract : 
            IGetAvailableCashOfUnitSuccessResultContract
        {
            public double Cash { get; set; }
        }
        
        public async Task<IGetAvailableCashOfUnitResultContract> Ask(
            IGetAvailableCashOfUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException(); 
            var operation = new CashOperation()
            {
                //InstanceId = input.InstanceId,
                //UnitId = input.UnitId,
                //From = input.From,
                //To = input.To,
                //OperationType = input.CashOperationType
            };
            await _context.CashOperations.AddAsync(operation, cancellationToken);
            return new GetAvailableCashOfUnitSuccessResultContract();
        }
    }
}