using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllClients
{
    public class GetAllClients : EndPointNodeBase<IGetAllClientsRequestContract, IGetAllClientsResultContract>
    {
        private readonly ApplicationContext _context;
        public GetAllClients(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetAllClientsResultContract>> Ask(
            IGetAllClientsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var clients = await _context.Clients.Where(c => c.InstanceId == input.InstanceId && c.Deleted == false).ToListAsync(cancellationToken: cancellationToken);
            if (input.ClientStateId != Guid.Empty)
            {
                clients = clients.Where(e => e.ClientStateId == input.ClientStateId).ToList();
            }
            if (input.ClientTypeId != Guid.Empty)
            {
                clients = clients.Where(e => e.ClientTypeId == input.ClientTypeId).ToList();
            }
            var result = clients!
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
                .ToList();
            return Pass(new GetAllClientsResultContract {Data = result});
        }
    }   
}