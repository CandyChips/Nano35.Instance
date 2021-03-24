using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetAllClients
{
    public class GetAllClientsRequest :
        EndPointNodeBase<
            IGetAllClientsRequestContract,
            IGetAllClientsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllClientsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllClientsSuccessResultContract : 
            IGetAllClientsSuccessResultContract
        {
            public IEnumerable<IClientViewModel> Data { get; set; }
        }
        
        public override async Task<IGetAllClientsResultContract> Ask(
            IGetAllClientsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await (_context.Clients
                .Where(c => c.InstanceId == input.InstanceId)
                .MapAllToAsync<IClientViewModel>());
            return new GetAllClientsSuccessResultContract() {Data = result};
        }
    }   
}