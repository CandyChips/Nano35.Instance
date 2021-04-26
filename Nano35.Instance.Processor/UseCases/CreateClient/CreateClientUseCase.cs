using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateClient
{
    public class CreateClientUseCase :
        EndPointNodeBase<
            ICreateClientRequestContract, 
            ICreateClientResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateClientUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateClientSuccessResultContract : 
            ICreateClientSuccessResultContract
        {
            
        }
        
        public override async Task<ICreateClientResultContract> Ask(
            ICreateClientRequestContract input,
            CancellationToken cancellationToken)
        {
            var client = new Client(){
                Id = input.NewId,
                InstanceId = input.InstanceId,
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone,
                Deleted = false,
                WorkerId = input.UserId,
                ClientStateId = input.ClientStateId,
                ClientTypeId =  input.ClientTypeId
            };
            await _context.AddAsync(client, cancellationToken);
            return new CreateClientSuccessResultContract();
        }
    }
}