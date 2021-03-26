using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreatePaymentOfSelle
{
    public class CreatePaymentOfSelleUseCase :
        EndPointNodeBase<
            ICreatePaymentOfSelleRequestContract,
            ICreatePaymentOfSelleResultContract>
    {
        private readonly ApplicationContext _context;

        public CreatePaymentOfSelleUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreatePaymentOfSelleSuccessResultContract : 
            ICreatePaymentOfSelleSuccessResultContract
        {
            
        }
        
        public override async Task<ICreatePaymentOfSelleResultContract> Ask(
            ICreatePaymentOfSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            var operation = new CashOperation()
            {
                Id = input.SelleId,
                UnitId = input.UnitId,
                Description = input.Description,
                InstanceId = input.InstanceId,
                WorkerId = input.WorkerId,
                Cash = input.Cash
            };
            await _context.CashOperations.AddAsync(operation, cancellationToken);
            return new CreatePaymentOfSelleSuccessResultContract();
        }
    }
}