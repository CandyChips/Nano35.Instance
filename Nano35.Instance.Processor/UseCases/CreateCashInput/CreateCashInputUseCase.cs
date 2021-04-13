using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateCashInput
{
    public class CreateCashInputUseCase :
        EndPointNodeBase<
            ICreateCashInputRequestContract,
            ICreateCashInputResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateCashInputUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateCashInputSuccessResultContract : 
            ICreateCashInputSuccessResultContract
        {
            
        }
        
        public override async Task<ICreateCashInputResultContract> Ask(
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
                WorkerId = input.WorkerId
            };
            
            await _context.CashOperations.AddAsync(operation, cancellationToken);
            return new CreateCashInputSuccessResultContract() {};
        }
    }
}