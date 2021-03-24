using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreatePaymentOfComing
{
    public class CreatePaymentOfComingRequest :
        EndPointNodeBase<
            ICreatePaymentOfComingRequestContract,
            ICreatePaymentOfComingResultContract>
    {
        private readonly ApplicationContext _context;

        public CreatePaymentOfComingRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreatePaymentOfComingSuccessResultContract : 
            ICreatePaymentOfComingSuccessResultContract
        {
            
        }
        
        public override async Task<ICreatePaymentOfComingResultContract> Ask(
            ICreatePaymentOfComingRequestContract input,
            CancellationToken cancellationToken)
        {
            var operation = new CashOperation()
            {
                Id = input.ComingId,
                UnitId = input.UnitId,
                Description = input.Description,
                InstanceId = input.InstanceId,
                WorkerId = input.WorkerId,
                Cash = input.Cash
            };
            await _context.CashOperations.AddAsync(operation, cancellationToken);
            return new CreatePaymentOfComingSuccessResultContract();
        }
    }
}