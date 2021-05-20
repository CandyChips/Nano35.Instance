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
            var clients = _context.Clients;
            if (input.ClientStateId != Guid.Empty)
            {
                clients = clients.Where(e => e.ClientStateId == input.ClientStateId) as DbSet<Client>;
            }
            if (input.ClientTypeId != Guid.Empty)
            {
                clients = clients!.Where(e => e.ClientTypeId == input.ClientTypeId) as DbSet<Client>;
            }
            var result = await clients!
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
            return Pass(new GetAllClientsResultContract {Data = result});
        }
    }   
}