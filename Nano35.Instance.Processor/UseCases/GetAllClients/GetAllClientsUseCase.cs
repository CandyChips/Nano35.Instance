using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllClients
{
    public class GetAllClientsUseCase : UseCaseEndPointNodeBase<IGetAllClientsRequestContract, IGetAllClientsSuccessResultContract>
    {
        private readonly ApplicationContext _context;
        public GetAllClientsUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetAllClientsSuccessResultContract>> Ask(
            IGetAllClientsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Clients
                .Where(c => c.InstanceId == input.InstanceId && c.Deleted == false)
                .Select(a => 
                    new ClientViewModel()
                        {Id = a.Id,
                         ClientState = a.ClientState.Name,
                         ClientStateId = a.ClientStateId,
                         ClientType = a.ClientType.Name,
                         ClientTypeId = a.ClientTypeId,
                         Email = a.Email,
                         Name = a.Name,
                         Phone = a.ClientProfile.Phone})
                .ToListAsync(cancellationToken);
            return new UseCaseResponse<IGetAllClientsSuccessResultContract>(new GetAllClientsSuccessResultContract() {Data = result});
        }
    }   
}