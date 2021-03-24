using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetAllInstances
{
    public class GetAllInstancesRequest :
        EndPointNodeBase<
            IGetAllInstancesRequestContract,
            IGetAllInstancesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllInstancesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllInstancesSuccessResultContract : 
            IGetAllInstancesSuccessResultContract
        {
            public IEnumerable<IInstanceViewModel> Data { get; set; }
        }

        public override async Task<IGetAllInstancesResultContract> Ask(
            IGetAllInstancesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Workers
                .Where(c => c.Id == input.UserId)
                .Select(a => a.Instance)
                .MapAllToAsync<IInstanceViewModel>());
            
            return new GetAllInstancesSuccessResultContract() {Data = result};
        }
    }
}