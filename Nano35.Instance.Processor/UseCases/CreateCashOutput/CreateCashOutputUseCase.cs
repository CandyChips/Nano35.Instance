using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateCashOutput
{
    public class CreateCashOutputUseCase :
        EndPointNodeBase<
            ICreateCashOutputRequestContract,
            ICreateCashOutputResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateCashOutputUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateCashOutputSuccessResultContract : 
            ICreateCashOutputSuccessResultContract
        {
            
        }
        
        public override async Task<ICreateCashOutputResultContract> Ask(
            ICreateCashOutputRequestContract input,
            CancellationToken cancellationToken)
        {
            var operation = new CashOperation()
            {
                Id = input.NewId,
                UnitId = input.UnitId,
                InstanceId = input.InstanceId,
                Type = (int) ApplicationContext.CashOperationTypes.Output,
                Description = input.Description,
                Cash = input.Cash,
                Date = DateTime.Now,
                WorkerId = input.WorkerId
            };
            await _context.CashOperations.AddAsync(operation, cancellationToken);
            return new CreateCashOutputSuccessResultContract();
        }
    }
}