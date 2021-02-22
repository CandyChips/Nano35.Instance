using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests.CreateCashInput
{
    public class CreateCashInputRequest :
        IPipelineNode<
            ICreateCashInputRequestContract,
            ICreateCashInputResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateCashInputRequest(
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
            var operation = new CashOperation()
            {
                Id = input.NewId,
                UnitId = input.UnitId,
                InstanceId = input.InstanceId,
                Type = (int) ApplicationContext.CashOperationTypes.Input,
                Description = input.Description,
                Cash = input.Cash,
                Date = DateTime.Now,
                WorkerId = input.UpdaterId
            };
            await _context.CashOperations.AddAsync(operation, cancellationToken);
            return new CreateCashInputSuccessResultContract();
        }
    }
}