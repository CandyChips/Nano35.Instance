using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetClientById
{
    public class GetClientByIdUseCase :
        EndPointNodeBase<
            IGetClientByIdRequestContract,
            IGetClientByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetClientByIdUseCase(
            ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<IGetClientByIdResultContract> Ask(
            IGetClientByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Clients
                .FirstOrDefaultAsync(f => f.Id == input.UnitId, cancellationToken: cancellationToken);
            return new GetClientByIdSuccessResultContract()
            {
                Data = new ClientViewModel()
                {
                    Id = result.Id, 
                    Email = result.Email,
                    Name = result.Name, 
                    Phone = result.Phone, 
                    ClientState = result.ClientState.Name,
                    ClientType = result.ClientType.Name,
                    ClientStateId = result.ClientStateId,
                    ClientTypeId = result.ClientTypeId
                }
            };
        }
    }
}