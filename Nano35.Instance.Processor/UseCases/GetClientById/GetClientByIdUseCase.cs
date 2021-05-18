using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetClientById
{
    public class GetClientByIdUseCase : UseCaseEndPointNodeBase<IGetClientByIdRequestContract,IGetClientByIdResultContract>
    {
        private readonly ApplicationContext _context;
        public GetClientByIdUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetClientByIdResultContract>> Ask(
            IGetClientByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Clients
                .FirstOrDefaultAsync(f => f.Id == input.UnitId, cancellationToken);
            
            return result == null ? 
                Pass("Клиент не найден.") :
                Pass(
                    new GetClientByIdResultContract()
                    {
                        Client = new ClientViewModel()
                            {Id = result.Id, 
                             Email = result.Email,
                             Name = result.Name, 
                             Phone = result.ClientProfile.Phone,
                             ClientState = result.ClientState.Name,
                             ClientType = result.ClientType.Name,
                             ClientStateId = result.ClientStateId,
                             ClientTypeId = result.ClientTypeId}
                    });
        }
    }
}